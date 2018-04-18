using Sleepy.Enums;
using Sleepy.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private void Shell_NavigateEvent(NavType NavigationType, Type ViewType, object parameter)
        {
            switch (NavigationType)
            {
                case NavType.NavigateTo:
                    NavigateTo(ViewType, parameter);
                    break;
                case NavType.NavigateBack:
                    GoBack();
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

        private void Shell_BackRequested(object sender, BackRequestedEventArgs e)
        {
            GoBack();
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
            NavigateEvent += Shell_NavigateEvent;
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
            SystemNavigationManager.GetForCurrentView().BackRequested += Shell_BackRequested;
        }

        private void SetUpDefaultViews()
        {
            SetContentFrame(typeof(TrackSleepView));
            SetMenuFrame();
        }

        private void SetContentFrame(Type ViewType)
        {
            contentFrame.Navigate(ViewType);
            RaiseNavigatedEvent();
        }

        private void AddCurrentPageToNavigationStack()
        {
            NavigationStack.Push((Page)contentFrame.Content);
        }

        private void SetMenuFrame()
        {
            MenuFrame.Navigate(typeof(MenuView));

        }

        private void NavigateTo(Type ViewType, object Parameter = null)
        {
            if (contentFrame?.CurrentSourcePageType != ViewType)
            {
                AddCurrentPageToNavigationStack();
                contentFrame.Navigate(ViewType);
                RaiseNavigatedEvent();
            }
          
        }

        private void GoBack()
        {
            if (NavigationStack.Count > 0)
            {
                var pageToNavigateTo = NavigationStack.Pop();
                Type pageType = pageToNavigateTo.GetType();
                contentFrame.GoBack();
                RaiseNavigatedEvent();
            }
        }
    }
    #endregion


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
