
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace SleepTightUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        DateTimeOffset sleepStart = new DateTime(2018,4,15,2,8,0);
        DateTimeOffset sleepEnd = new DateTime(2018, 4, 15, 9, 53, 0);
        TimeSpan expectedSleep = new TimeSpan(7, 45, 0);

        [TestMethod]
        public void DifferenceBetweenTimes()
        {
            TimeSpan sleepDone = sleepEnd - sleepStart;
            Logger.LogMessage("Sleep done: " + sleepDone);
            int hours = sleepDone.Hours;
            int minutes = sleepDone.Minutes;
            string errorMessage = "Sleep time is not expected sleep";
            Assert.IsTrue(sleepDone == expectedSleep, errorMessage);
        }
    }
}
