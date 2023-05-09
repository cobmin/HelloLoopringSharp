namespace HelloLoopringSharp.Models
{
    public class EddsaSignatureForMintRequest
    {
        public string? CurrentCid { get; set; }
        public int AccountId { get; set; }
        public string? Owner { get; set; }
        public string? CollectionAddress { get; set; }
        public int NftRoyaltyPercentage { get; set; }
        public string? Exchange { get; set; }
        public int NftAmount { get; set; }
        public int MaxFeeTokenId { get; set; }
        public string? Fee { get; set; }
        public long ValidUntil { get; set; }
        public int OffchainId { get; set; }
        public string? LayerTwoPrivateKey { get; set; }

    }
}
