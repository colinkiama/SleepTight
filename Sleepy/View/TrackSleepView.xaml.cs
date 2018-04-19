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

namespace Sleepy.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrackSleepView : Page
    {
        public TrackSleepView()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (ViewModel.CurrentSleepData.Count < 1)
            {
                this.ColumnChart.Visibility = Visibility.Collapsed;
            }
            else
            {
                NoSleepTrackStackPanel.Visibility = Visibility.Collapsed;
            }

        }

        private void AddSleepDataButton_Click(object sender, RoutedEventArgs e)
        {
            Shell.Navigate(typeof(AddSleepDataView));
        }
    }

}
