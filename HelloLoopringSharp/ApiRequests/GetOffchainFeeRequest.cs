namespace LoopringBatchMintNFTs.ApiResponses
{
    public class GetOffchainFeeRequest
    {
        public int? AccountId { get; set; }
        public int? RequestType { get; set; }
        public string? TokenAddress { get; set; }
    }
}