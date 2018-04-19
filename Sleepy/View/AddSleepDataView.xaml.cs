using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class AddSleepDataView : Page
    {
        public AddSleepDataView()
        {
            this.InitializeComponent();
        }


        private void FinishAddingSleepDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Getting start of sleep
            DateTimeOffset startDate = SleepStartDatePicker.Date;
            TimeSpan startTime = SleepEndTimePicker.Time;
            DateTime sleepStart = new DateTime(startDate.Year, startDate.Month, startDate.Day,startTime.Hours,startTime.Minutes,0);

            // Getting end of sleep 
            DateTimeOffset endDate = SleepEndDatePicker.Date;
            TimeSpan endTime = SleepEndTimePicker.Time;
            DateTime sleepEnd = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hours, endTime.Minutes, 0);

            // Getting quality of sleep
            int ratingValueAsInt = (int)SleepQualityRatingControl.Value;
            // TODO: Case Statement for values 1 to 5 paired to 1 to 5 star sleep quality.

            
        }
    }
}
