using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelloLoopringSharp.ApiResponses
{
    public class ApiKeyResponse
    {
        [JsonPropertyName("apiKey")]
        public string? ApiKey { get; set; }
    }
}
