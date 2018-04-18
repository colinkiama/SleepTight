using Sleepy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sleepy.ViewModel
{
    class TrackSleepViewModel : INotifyPropertyChanged
    {
        #region Fields

        ObservableCollection<Sleep> sleeps;
        ObservableCollection<Sleep> _currentSleepData;
        #endregion

        #region Properties

        public TrackSleepViewModel()
        {
            // Get "sleeps" from saved list with sleep data
            var loadedSleepData = Sleep.ParseLoadedSleepData(App.fileIOHelper.loadedSaveData);
            sleeps = new ObservableCollection<Sleep>(loadedSleepData);
            _currentSleepData = new ObservableCollection<Sleep>();
            GetSleepDataForWeek();
            
        }

       
        public ObservableCollection<Sleep> CurrentSleepData
        {
            get
            {
                return _currentSleepData;
            }
            set
            {
                _currentSleepData = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Event Methods

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Methods
        //start creating methods that return a list
        public void GetSleepDataForWeek()
        {
            var currentDate = DateTime.Now;
            var firstDateToView = currentDate.AddDays(-6);
            var sleepDataForTheWeek = sleeps.Where(p => p.SleepStart >= firstDateToView && p.SleepStart <= currentDate);
            _currentSleepData.Clear();
            foreach (var sleepData in sleepDataForTheWeek)
            {
                _currentSleepData.Add(sleepData);
            }
        }

        public void GetSleepDataForMonth()
        {
            var currentDate = DateTime.Now;
            var firstDateToView = currentDate.AddMonths(-1);
            var sleepDataForTheWeek = sleeps.Where(p => p.SleepStart >= firstDateToView && p.SleepStart <= currentDate);
            _currentSleepData.Clear();
            foreach (var sleepData in sleepDataForTheWeek)
            {
                _currentSleepData.Add(sleepData);
            }
        }

        public void GetSleepDataForYear()
        {
            var currentDate = DateTime.Now;
            var firstDateToView = currentDate.AddYears(-1);
            var sleepDataForTheWeek = sleeps.Where(p => p.SleepStart >= firstDateToView && p.SleepStart <= currentDate);
            _currentSleepData.Clear();
            foreach (var sleepData in sleepDataForTheWeek)
            {
                _currentSleepData.Add(sleepData);
            }
        }

        public void GetSleepDataForAllTime()
        {
            _currentSleepData.Clear();
            foreach (var sleepData in sleeps)
            {
                _currentSleepData.Add(sleepData);
            }
        }

        #endregion

        #region Test Methods
        private void FillCurrentDataWithFakeData()
        {
            var sleepToAdd = new Sleep(DateTime.Now.AddHours(-8), DateTime.Now, Enums.SleepQuality.ThreeStars, "Testing");
            _currentSleepData.Clear();
            _currentSleepData.Add(sleepToAdd);
        }
        #endregion

    }
}