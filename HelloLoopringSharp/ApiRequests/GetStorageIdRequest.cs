namespace HelloLoopringSharp.ApiRequests
{
    public class GetStorageIdRequest
    {
        public int AccountId { get; set; }
        public int SellTokenId { get; set; }
        public bool? MaxNext { get; set; }
    }
}
