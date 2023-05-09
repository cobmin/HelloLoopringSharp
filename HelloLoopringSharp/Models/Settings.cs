namespace LoopringBatchMintNFTs.Models
{
    public class Settings
    {
        public string LayerOnePrivateKey { get; set; }
        public int AccountId { get; set; }
        public string Owner { get; set; }
        public int Network { get; set; }
        public int MaxFeeTokenId { get; set; }
        public int NftType { get; set; }
        public int NftRoyaltyPercentage { get; set; }
        public int NftAmount { get; set; }
    }
}