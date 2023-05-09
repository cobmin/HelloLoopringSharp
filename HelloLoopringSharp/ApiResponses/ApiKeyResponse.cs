using System.Text.Json.Serialization;

namespace HelloLoopringSharp.ApiResponses
{
    public class ApiKeyResponse
    {
        [JsonPropertyName("apiKey")]
        public string? ApiKey { get; set; }
    }
}
