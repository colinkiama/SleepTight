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
        public static event EventHandler NavigateEvent;
        public static event EventHandler GoBackEvent;

        

        internal static void Navigate(Type ViewType, object parameter = null)
        {
            //Use this to invoke the NavigateEvent
            
        }

        internal static void NavigateBack()
        {
            //Use this to invoke the GoBackEvent
        }

        public Shell()
        {
            this.InitializeComponent();
            NavigationStack = new Stack<Page>();

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

        public void NavigateTo(Type ViewType)
        {
            contentFrame.Navigate(ViewType);
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
}
