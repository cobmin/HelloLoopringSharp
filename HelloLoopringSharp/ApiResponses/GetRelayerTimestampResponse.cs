using System.Text.Json.Serialization;

namespace LoopringBatchMintNFTs.ApiResponses
{
    public class GetRelayerTimestampResponse
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
