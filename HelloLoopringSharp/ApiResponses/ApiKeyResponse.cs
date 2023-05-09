using System.Text.Json.Serialization;

namespace LoopringBatchMintNFTs.ApiResponses
{
    public class ApiKeyResponse
    {
        [JsonPropertyName("apiKey")]
        public string? ApiKey { get; set; }
    }
}
