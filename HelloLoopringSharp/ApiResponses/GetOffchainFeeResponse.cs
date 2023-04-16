using System.Text.Json.Serialization;

namespace HelloLoopringSharp.ApiResponses
{
    public class FeeResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
        [JsonPropertyName("fee")]
        public string? Fee { get; set; }
        [JsonPropertyName("discount")]
        public int? Discount { get; set; }
    }

    public class GetOffchainFeeResponse
    {
        [JsonPropertyName("gasPrice")]
        public string? GasPrice { get; set; }
        [JsonPropertyName("fees")]
        public List<FeeResponse>? Fees { get; set; }
    }
}