using System.Text.Json.Serialization;

namespace Maize
{
    public class PostMintNftResponse
    {
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
        [JsonPropertyName("nftTokenId")]
        public int NftTokenId { get; set; }
        [JsonPropertyName("nftId")]

        public string? NftId { get; set; }
        [JsonPropertyName("nftData")]
        public string? NftData { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("isIdempotent")]
        public bool IsIdempotent { get; set; }
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }
        [JsonPropertyName("storageId")]
        public int StorageId { get; set; }
        [JsonPropertyName("metadataCid")]
        public string? MetadataCid { get; set; }
        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }
    }
}
