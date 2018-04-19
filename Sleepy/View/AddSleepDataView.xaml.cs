using Sleepy.Enums;
using Sleepy.Model;
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
            SleepQualityRatingControl.Value = 1;
        }


        private async void FinishAddingSleepDataButton_Click(object sender, RoutedEventArgs e)
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
            SleepQuality qualityOfSleep = new SleepQuality();
            switch (ratingValueAsInt)
            {
                case 1:
                    qualityOfSleep = SleepQuality.OneStar;
                    break;
                case 2:
                    qualityOfSleep = SleepQuality.TwoStars;
                    break;
                case 3:
                    qualityOfSleep = SleepQuality.ThreeStars;
                    break;
                case 4:
                    qualityOfSleep = SleepQuality.FourStars;
                    break;
                case 5:
                    qualityOfSleep = SleepQuality.FiveStars;
                    break;
            }

            Sleep sleepToAdd;
            if (NotesTextBox.Text.Trim().Equals(string.Empty))
            {
                sleepToAdd = new Sleep(sleepStart, sleepEnd, qualityOfSleep);

            }
            else
            {
                sleepToAdd = new Sleep(sleepStart, sleepEnd, qualityOfSleep, NotesTextBox.Text);
            }

            await ViewModel.AddNewSleep(sleepToAdd);

            Shell.Navigate(typeof(TrackSleepView));

        }
    }
}
