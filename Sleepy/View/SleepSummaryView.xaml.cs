using Sleepy.Model;
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
    public sealed partial class SleepSummaryView : Page
    {
        public SleepSummaryView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var sleepCompleted = (Sleep)e.Parameter;
            var sleepStartTime = sleepCompleted.SleepStart.TimeOfDay;
            var sleepEndTime = sleepCompleted.SleepEnd.TimeOfDay;
            var hoursSlept = sleepCompleted.HoursSleptFor;

            SleepStartTextBlock.Text = $"{sleepStartTime.Hours}:{sleepStartTime.Minutes}";
            SleepEndTextBlock.Text = $"{sleepEndTime.Hours}:{sleepEndTime.Minutes}";
            HoursSleptTextBlock.Text = hoursSlept.ToString();




        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
