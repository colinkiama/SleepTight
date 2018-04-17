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
        }

        public ObservableCollection<Sleep> CurrentSleepData
        {
            get
            {
                return CurrentSleepData;
            }
            set {
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
        
        #endregion



    }
}