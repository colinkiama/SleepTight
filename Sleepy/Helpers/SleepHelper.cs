using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sleepy.Helpers
{

    public class SleepHelper
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private const string isSleepingKey = "isSleeping";


        public static void GoToSleep()
        {
            localSettings.Values[isSleepingKey] = true;
        }

        public static void WakeUp()
        {
            localSettings.Values[isSleepingKey] = false;
        }

        public static bool CheckIfSleeping()
        {
            bool isSleeping = (bool)localSettings.Values[isSleepingKey];
            return isSleeping;
        }
    }
}
