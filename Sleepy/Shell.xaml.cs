using Sleepy.Enums;
using Sleepy.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Shell : Page
    {
        public Stack<Page> NavigationStack;

        //TODO: Create events that carry page types and parameters. When the events are raised,
        //You start navigate navigating using the args specified
        //Args should be: Type ViewType and object Parameter
        public static event NavEventHandler NavigateEvent;

        public delegate void NavEventHandler(NavType NavigationType, Type ViewType = null, object parameter = null);

        internal static void Navigate(Type viewType, object parameter = null)
        {
            NavigateEvent?.Invoke(NavType.NavigateTo, viewType, parameter);


        }


        internal static void NavigateBack()
        {
            NavigateEvent?.Invoke(NavType.NavigateBack);
        }

        public Shell()
        {
            this.InitializeComponent();
            NavigationStack = new Stack<Page>();
            NavigateEvent += Shell_NavigateEvent;

        }

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                // Handle parameters when app becomes more advanced
                
            }
            SetUpDefaultViews();
            
        }

        private void SetUpDefaultViews()
        {
            SetContentFrame(typeof(TrackSleepView));
            SetMenuFrame();
        }

        private void SetContentFrame(Type ViewType)
        {
            contentFrame.Navigate(ViewType);
            AddCurrentPageToNavigationStack();
        }

        private void AddCurrentPageToNavigationStack()
        {
            NavigationStack.Push((Page)contentFrame.Content);
        }

        private void SetMenuFrame()
        {
            MenuFrame.Navigate(typeof(MenuView));
        }

        public void NavigateTo(Type ViewType, object Parameter = null)
        {
            contentFrame.Navigate(ViewType,Parameter);
            AddCurrentPageToNavigationStack();
        }

        public void GoBack()
        {
            if (NavigationStack.Count > 0)
            {
                var pageToNavigateTo = NavigationStack.Pop();
                contentFrame.Navigate(pageToNavigateTo.GetType());
            }
        }
    }

    public class NavEventArgs
    {
        public NavEventArgs(NavType navigationType,Type viewType = null, object paramenter = null)
        {
            _navigationType = navigationType;
            _viewType = viewType;
            _parameter = paramenter;

        }

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

    }
}
