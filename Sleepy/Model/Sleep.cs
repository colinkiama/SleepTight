using Sleepy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleepy.Model
{
    public class Sleep
    {
        #region Properties
        public DateTime SleepStart { get; set; }
        public DateTime SleepEnd { get; set; }
        public SleepQuality QualityOfSleep { get; set; }
        public string Notes { get; set; }
        #endregion

        #region Constructors
        public Sleep(DateTime sleepStart, DateTime sleepEnd, SleepQuality qualityOfSleep)
        {
            SleepStart = sleepStart;
            SleepEnd = sleepEnd;
            QualityOfSleep = qualityOfSleep;
            Notes = String.Empty;
        }

        public Sleep(DateTime sleepStart, DateTime sleepEnd, SleepQuality qualityOfSleep, string notes)
        {
            SleepStart = sleepStart;
            SleepEnd = sleepEnd;
            QualityOfSleep = qualityOfSleep;
            Notes = notes;
        }
        #endregion

        #region Methods


        public override string ToString()
        {
            return $"{SleepStart} {SleepEnd} {QualityOfSleep} {Notes}";
        }

        public static TimeSpan CalculateSleepLength(DateTime sleepStart, DateTime sleepEnd)
        {
            TimeSpan sleepCompleted = sleepEnd - sleepStart;
            return sleepCompleted;
        }
        #endregion

        #region Static Methods
        public static List<Sleep> GetSleepDataFromString(IList<string> dataToGetSleepDataFrom)
        {
            // Turn the list of strings into a list of sleep data
            List<Sleep> sleepData = new List<Sleep>();
        }
        #endregion
    }
}
