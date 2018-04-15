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
        public DateTime SleepStart { get; set; }
        public DateTime SleepEnd { get; set; }
        public SleepQuality QualityOfSleep { get; set; }
        public string Notes { get; set; }

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

        public override string ToString()
        {
            return $"Slept from {SleepStart} to {SleepEnd}";
        }

        public static TimeSpan CalculateSleepLength(DateTime sleepStart, DateTime sleepEnd)
        {
            TimeSpan sleepCompleted = sleepEnd - sleepStart;
            return sleepCompleted;
        }
    }
}
