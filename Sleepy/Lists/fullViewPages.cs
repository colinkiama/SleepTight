using Sleepy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleepy.Lists
{
    public class FullViewPages
    {
        public static Type[] pageList  { get; private set; } = { typeof(SleepView), typeof(SleepSummaryView) };
    }
}
