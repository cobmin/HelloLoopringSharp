using LoopringBatchMintNFTs.Models;

namespace LoopringBatchMintNFTs.ApiRequests
{
    public class PostMintNftRequest
    {
        public string? Exchange { get; set; }
        public int MinterId { get; set; }
        public string? MinterAddress { get; set; }
        public int ToAccountId { get; set; }
        public string? ToAddress { get; set; }
        public int NftType { get; set; }
        public string? TokenAddress { get; set; }
        public string? NftId { get; set; }
        public int? Amount { get; set; }
        public long ValidUntil { get; set; }
        public int RoyaltyPercentage { get; set; }
        public int StorageId { get; set; }
        public MaxFee? MaxFee { get; set; }
        public string? ForceToMint { get; set; }
        public string? RoyaltyAddress { get; set; }
        public CounterFactualNftInfo? CounterFactualNftInfo { get; set; }
        public string? EddsaSignature { get; set; }
    }
    public class MaxFee
    {
        public int TokenId { get; set; }
        public string Amount { get; set; }
    }
}
