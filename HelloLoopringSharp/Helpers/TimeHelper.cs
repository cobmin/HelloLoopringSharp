namespace LoopringBatchMintNFTs.Helpers
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
