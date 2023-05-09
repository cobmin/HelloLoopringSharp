using System.Text.Json.Serialization;

namespace LoopringBatchMintNFTs.ApiResponses
{
    public class Cached
    {
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }
        [JsonPropertyName("tileUri")]
        public string? TileUri { get; set; }
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
    }

    public class Collections
    {
        [JsonPropertyName("collection")]
        public Collection? Collection { get; set; }
        [JsonPropertyName("count")]
        public int? Count { get; set; }
    }

    public class Collection
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("owner")]
        public string? Owner { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("contractAddress")]
        public string? ContractAddress { get; set; }
        [JsonPropertyName("collectionAddress")]
        public string? CollectionAddress { get; set; }
        [JsonPropertyName("baseUri")]
        public string? BaseUri { get; set; }
        [JsonPropertyName("nftFactory")]
        public string? NftFactory { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("banner")]
        public string? Banner { get; set; }
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }
        [JsonPropertyName("tileUri")]
        public string? TileUri { get; set; }
        [JsonPropertyName("cached")]
        public Cached? Cached { get; set; }
        [JsonPropertyName("deployStatus")]
        public string? DeployStatus { get; set; }
        [JsonPropertyName("nftType")]
        public string? NftType { get; set; }
        [JsonPropertyName("times")]
        public Times? Times { get; set; }
        [JsonPropertyName("extra")]
        public Extra? Extra { get; set; }
    }

    public class Extra
    {
        [JsonPropertyName("properties")]
        public Properties? Properties { get; set; }
        [JsonPropertyName("mintChannel")]
        public string? MintChannel { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("isLegacy")]
        public bool? IsLegacy { get; set; }
        [JsonPropertyName("isPublic")]
        public bool? IsPublic { get; set; }
        [JsonPropertyName("isCounterFactualNFT")]
        public bool? IsCounterFactualNFT { get; set; }
        [JsonPropertyName("isMintable")]
        public bool? IsMintable { get; set; }
        [JsonPropertyName("isEditable")]
        public bool? IsEditable { get; set; }
        [JsonPropertyName("isDeletable")]
        public bool? IsDeletable { get; set; }
    }

    public class GetUserCollectionsResponse
    {
        [JsonPropertyName("collections")]
        public List<Collections>? Collections { get; set; }
        [JsonPropertyName("totalNum")]
        public int? TotalNum { get; set; }
    }

    public class Times
    {
        [JsonPropertyName("createdAt")]
        public object? CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public object? UpdatedAt { get; set; }
    }
    public class NftTokenInfo
    {
        [JsonPropertyName("nftData")]
        public string? NftData { get; set; }
        [JsonPropertyName("minter")]
        public string? Minter { get; set; }
        [JsonPropertyName("nftType")]
        public string? NftType { get; set; }
        [JsonPropertyName("tokenAddress")]
        public string? TokenAddress { get; set; }
        [JsonPropertyName("nftId")]
        public string? NftId { get; set; }
        [JsonPropertyName("creatorFeeBips")]
        public int? CreatorFeeBips { get; set; }
        [JsonPropertyName("royaltyPercentage")]
        public int? RoyaltyPercentage { get; set; }
        [JsonPropertyName("originalRoyaltyPercentage")]
        public int? OriginalRoyaltyPercentage { get; set; }
        [JsonPropertyName("status")]
        public bool? Status { get; set; }
        [JsonPropertyName("nftFactory")]
        public string? NftFactory { get; set; }
        [JsonPropertyName("nftOwner")]
        public string? NftOwner { get; set; }
        [JsonPropertyName("nftBaseUri")]
        public string? NftBaseUri { get; set; }
        [JsonPropertyName("royaltyAddress")]
        public string? RoyaltyAddress { get; set; }
        [JsonPropertyName("originalMinter")]
        public string? OriginalMinter { get; set; }
        [JsonPropertyName("createdAt")]
        public object? CreatedAt { get; set; }
        [JsonPropertyName("total")]
        public int? Total { get; set; }
    }

    public class CollectionInformation
    {
        [JsonPropertyName("totalNum")]
        public int? TotalNum { get; set; }
        [JsonPropertyName("nftTokenInfos")]
        public List<NftTokenInfo>? NftTokenInfos { get; set; }
    }

    public class NftInformation
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("total")]
        public int? Total { get; set; }
        [JsonPropertyName("nftData")]
        public string? NftData { get; set; }
        [JsonPropertyName("nftId")]
        public string? NftId { get; set; }
        [JsonPropertyName("minter")]
        public string? Minter { get; set; }
        [JsonPropertyName("tokenAddress")]
        public string? TokenAddress { get; set; }
        [JsonPropertyName("royaltyPercentage")]
        public int? RoyaltyPercentage { get; set; }
        [JsonPropertyName("attributes")]
        public List<NftAttribute>? Attributes { get; set; }
    }
    public class OwnedCollection
    {
        [JsonPropertyName("collection")]
        public Collection? Collection { get; set; }
        [JsonPropertyName("count")]
        public int? Count { get; set; }
    }
    public class UserOwnedCollections
    {
        [JsonPropertyName("collections")]
        public List<OwnedCollection>? Collections { get; set; }
        [JsonPropertyName("totalNum")]
        public int? TotalNum { get; set; }
    }
    public class NftAttribute
    {
        [JsonPropertyName("trait_type")]
        public string? Trait_type { get; set; }
        [JsonPropertyName("value")]
        public object? Value { get; set; } //value can be either string or int
    }

}
