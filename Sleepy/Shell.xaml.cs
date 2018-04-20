using Microsoft.Toolkit.Uwp.UI.Animations;
using Sleepy.Enums;
using Sleepy.Lists;
using Sleepy.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sleepy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page, INotifyPropertyChanged
    {
        #region Fields
        private Stack<Page> NavigationStack;
        #endregion

        #region Properties

        public bool CanGoBack { get => NavigationStack.Count > 0; }
        public bool isFullViewActive { get => fullViewFrame.Content != null; }

        #endregion

        #region Events
        public static event NavEventHandler NavigateEvent;
        public static event EventHandler Navigated;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Event Handlers
        public delegate void NavEventHandler(NavType NavigationType, Type ViewType = null, object parameter = null);
        #endregion

        #region Event Methods
        private async void Shell_NavigateEventAsync(NavType NavigationType, Type ViewType, object parameter)
        {
            switch (NavigationType)
            {
                case NavType.NavigateTo:
                    await NavigateToAsync(ViewType, parameter);
                    break;
                case NavType.NavigateBack:
                    await GoBackAsync();
                    break;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaiseNavigatedEvent()
        {
            Navigated?.Invoke(this, EventArgs.Empty);
        }

        private async void Shell_BackRequestedAsync(object sender, BackRequestedEventArgs e)
        {
            await GoBackAsync();
        }

        private void Shell_Navigated(object sender, EventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        #endregion

        #region Static Methods
        internal static void Navigate(Type viewType, object parameter = null)
        {
            NavigateEvent?.Invoke(NavType.NavigateTo, viewType, parameter);
        }


        internal static void NavigateBack()
        {
            NavigateEvent?.Invoke(NavType.NavigateBack);
        }
        #endregion

        #region Constructors
        public Shell()
        {
            this.InitializeComponent();
            NavigationStack = new Stack<Page>();
            NavigateEvent += Shell_NavigateEventAsync;
            Navigated += Shell_Navigated;
        }


        #endregion

        #region Override Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                // Handle parameters when app becomes more advanced

            }
            SetUpDefaultViews();
            HookUpBackButton();

        }




        #endregion

        #region Private Methods

        private void HookUpBackButton()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += Shell_BackRequestedAsync;
        }

        private void SetUpDefaultViews()
        {
            SetContentFrame(typeof(TrackSleepView));
            SetMenuFrame();
            SetUpFullViewFrameForAnimation();
        }

        private void SetUpFullViewFrameForAnimation()
        {
            App.animationHelper.PrepForScaleAnim(fullViewFrame);
        }

        private void SetContentFrame(Type ViewType)
        {
            contentFrame.Navigate(ViewType);
            RaiseNavigatedEvent();
        }

        private void AddCurrentContentPageToNavigationStack()
        {
            NavigationStack.Push((Page)contentFrame.Content);
        }

        private void SetMenuFrame()
        {
            MenuFrame.Navigate(typeof(MenuView));

        }

        private async Task NavigateToAsync(Type ViewType, object Parameter = null)
        {
            if (contentFrame?.CurrentSourcePageType != ViewType)
            {
                bool isFullViewPage = CheckIfFullViewPage(ViewType);
                AddCurrentPageToNavigationStack();
                if (isFullViewPage)
                {
                    await HandleFullViewNavigationAsync(ViewType);
                }
                else
                {
                    contentFrame.Navigate(ViewType);
                }
                RaiseNavigatedEvent();
            }

        }

        private void AddCurrentPageToNavigationStack()
        {
            if (isFullViewActive)
            {
                AddCurrentFullViewPageToNavigationStack();
            }
            else
            {
                AddCurrentContentPageToNavigationStack();
            }
        }

        private void AddCurrentFullViewPageToNavigationStack()
        {
            NavigationStack.Push((Page)fullViewFrame.Content);
        }

        private async Task HandleFullViewNavigationAsync(Type viewType)
        {
            if (!isFullViewActive)
            {
                fullViewFrame.Navigate(viewType);
                fullViewFrame.Visibility = Visibility.Visible;
                await App.animationHelper.ScaleAnimation(fullViewFrame);
            }
            else
            {
                fullViewFrame.Navigate(viewType);
                if (viewType == typeof(SleepSummaryView))
                {
                    NavigationStack.Clear();
                }
            }
            RaiseNavigatedEvent();
        }

        private bool CheckIfFullViewPage(Type viewType)
        {
            return (FullViewPages.pageList.Contains(viewType));
        }

        private async Task GoBackAsync()
        {
            if (NavigationStack.Count > 0)
            {
                var pageToNavigateTo = NavigationStack.Pop();
                Type pageType = pageToNavigateTo.GetType();
                if (fullViewFrame.Content != null)
                {
                    await HandleFullViewFrameNavigateBackAsync(pageType);
                }
                else
                {
                    contentFrame.GoBack();
                }

                RaiseNavigatedEvent();
            }
        }

        private async Task HandleFullViewFrameNavigateBackAsync(Type pageType)
        {
            bool isPrevPageFullView = CheckIfFullViewPage(pageType);
            if (isPrevPageFullView)
            {
                fullViewFrame.GoBack();
            }
            else
            {
                await App.animationHelper.ScaleDownAnimation(fullViewFrame);
                fullViewFrame.Visibility = Visibility.Collapsed;
                fullViewFrame.Content = null;
            }
            
        }
        #endregion
    }



    public class NavEventArgs
    {

        #region Constructors
        public NavEventArgs(NavType navigationType, Type viewType = null, object paramenter = null)
        {
            _navigationType = navigationType;
            _viewType = viewType;
            _parameter = paramenter;

        }
        #endregion

        #region Properties
        private NavType _navigationType;

        public NavType NavigationType
        {
            get { return _navigationType; }
        }

        private Type _viewType;

        public Type ViewType
        {
            get { return _viewType; }
        }

        private object _parameter;

        public object Parameter
        {
            get { return _parameter; }
        }
        #endregion


    }



}
