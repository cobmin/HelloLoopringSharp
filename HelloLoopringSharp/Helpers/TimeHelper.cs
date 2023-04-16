using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloLoopringSharp.Helpers
{
    public class TimeHelper
    {
        public static long AddTwoMonthsToCurrentRelayerTime(long relayerTimestamp)
        {
            DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeMilliseconds(relayerTimestamp);
            DateTimeOffset utcTimePlusTwoMonths = utcTime.AddMonths(2);
            long unixTimestampPlusTwoMonths = utcTimePlusTwoMonths.ToUnixTimeSeconds();
            return unixTimestampPlusTwoMonths;
        }
    }
}
