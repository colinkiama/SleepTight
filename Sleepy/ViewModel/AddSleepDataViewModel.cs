using Sleepy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleepy.ViewModel
{
    class AddSleepDataViewModel
    {
        public ObservableCollection<Sleep> sleeps { get; private set; }

        public AddSleepDataViewModel()
        {
            var loadedSleepData = Sleep.ParseLoadedSleepData(App.fileIOHelper.loadedSaveData);
            sleeps = new ObservableCollection<Sleep>(loadedSleepData);
        }

        public async Task AddNewSleep(Sleep sleepToAdd)
        {
            sleeps.Add(sleepToAdd);
            await App.fileIOHelper.saveSleepDataAsync(sleeps);
        }


    }
}
