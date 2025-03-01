﻿using System.Text.Json.Serialization;

namespace LoopringBatchMintNFTs.ApiResponses
{
    public class GetStorageIdResponse
    {
        [JsonPropertyName("orderId")]
        public int OrderId {get;set;}
        [JsonPropertyName("offchainId")]
        public int OffchainId { get; set; }
    }
}
