using Sleepy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleepy.Model
{
    public class Sleep: IComparable<Sleep>
    {
        #region Properties
        public DateTime SleepStart { get; set; }
        public DateTime SleepEnd { get; set; }
        public SleepQuality QualityOfSleep { get; set; }
        public string Notes { get; set; }
        public double HoursSleptFor { get { return new TimeSpan(SleepEnd.Ticks).TotalHours - new TimeSpan(SleepStart.Ticks).TotalHours; } }
        public string DaySleptOn { get { return SleepStart.DayOfWeek.ToString(); } }
        #endregion

        #region Constructors
        public Sleep(DateTime sleepStart, DateTime sleepEnd)
        {
            SleepStart = sleepStart;
            SleepEnd = sleepEnd;
        }

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
            return $"{SleepStart},{SleepEnd},{QualityOfSleep},{Notes}";
        }

        public static TimeSpan CalculateSleepLength(DateTime sleepStart, DateTime sleepEnd)
        {
            TimeSpan sleepCompleted = sleepEnd - sleepStart;
            return sleepCompleted;
        }
        #endregion

        #region Static Methods
        public static List<Sleep> ParseLoadedSleepData(IList<string> dataToGetSleepDataFrom)
        {
            List<Sleep> sleepDataToReturn = new List<Sleep>();

            if (dataToGetSleepDataFrom != null)
            {
                foreach (var item in dataToGetSleepDataFrom)
                {
                    string[] sleepProperties = item.Split(',');
                    var sleepToAdd = ParseSleepDataFromString(sleepProperties);
                    sleepDataToReturn.Add(sleepToAdd);
                }
            }
            

            return sleepDataToReturn;
        }

        static Sleep ParseSleepDataFromString(string[] sleepProperties)
        {
            var sleepStart = DateTime.Parse(sleepProperties[0]);
            var sleepEnd = DateTime.Parse(sleepProperties[1]);
            var qualityOfSleep = (SleepQuality)Enum.Parse(typeof(SleepQuality), sleepProperties[2]);
            var notes = sleepProperties[3];
            var parsedSleepData = new Sleep(sleepStart, sleepEnd, qualityOfSleep, notes);
            return parsedSleepData;
        }


        #endregion

        #region IComparable Methods
        public int CompareTo(Sleep other)
        {
            var currentSleepDate = SleepStart.Date;
            var otherSleepDate = other.SleepStart.Date;
            TimeSpan differenceBetweenDates = currentSleepDate - otherSleepDate;
            int result = (int)differenceBetweenDates.TotalDays;
            return result;
        }
        #endregion
    }
}
