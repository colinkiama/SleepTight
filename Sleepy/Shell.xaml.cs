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

        private void SetContentFrame(Type pageType)
        {
            contentFrame.Navigate(pageType);
        }

        private void SetMenuFrame()
        {
            MenuFrame.Navigate(typeof(MenuView));
        }

       
    }
}
