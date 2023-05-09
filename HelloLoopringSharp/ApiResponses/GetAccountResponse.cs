using System.Text.Json.Serialization;

namespace HelloLoopringSharp.ApiResponses
{

    public class PublicKey
    {
        [JsonPropertyName("x")]
        public string? X { get; set; }
        [JsonPropertyName("y")]
        public string? Y { get; set; }
    }

    public class GetAccountResponse
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }
        [JsonPropertyName("owner")]
        public string? Owner { get; set; }
        public bool Frozen { get; set; }
        [JsonPropertyName("publicKey")]
        public PublicKey? PublicKey { get; set; }
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        [JsonPropertyName("nonce")]
        public int Nonce { get; set; }
        [JsonPropertyName("keyNonce")]
        public int KeyNonce { get; set; }
        [JsonPropertyName("keySeed")]
        public string? KeySeed { get; set; }
    }
}
