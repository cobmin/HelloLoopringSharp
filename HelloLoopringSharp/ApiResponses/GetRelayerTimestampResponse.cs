using System.Text.Json.Serialization;

namespace HelloLoopringSharp.ApiResponses
{
    public class GetRelayerTimestampResponse
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
