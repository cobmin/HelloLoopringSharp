using LoopringBatchMintNFTs.ApiRequests;
using LoopringBatchMintNFTs.ApiResponses;
using LoopringBatchMintNFTs.Helpers;
using LoopringBatchMintNFTs.Models;
using Maize;
using Newtonsoft.Json;
using RestSharp;

namespace LoopringBatchMintNFTs.Client
{
    public class LoopringClient : ILoopringClient, IDisposable
    {
        readonly RestClient _client;
        string _baseUrl;
        public LoopringClient(string url)
        {
            _client = new RestClient(url);
            _baseUrl = url;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<GetAccountResponse> GetAccount(GetAccountRequest getAccountRequest)
        {
            var request = new RestRequest("/api/v3/account");
            request.AddParameter("owner", getAccountRequest.Owner);
            request.AddParameter("accountId", getAccountRequest.AccountId);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<GetAccountResponse>(response.Content);
            return data;
        }

        public async Task<ApiKeyResponse> GetApiKey(string layerTwoPrivateKey, int accountId)
        {       
            var apiSig = UrlHelper.Sign(
               layerTwoPrivateKey,
              HttpMethod.Get,
              new List<(string Key, string Value)>() { ("accountId", accountId.ToString()) },
              null,
              "/api/v3/apiKey",
              _baseUrl);
            var request = new RestRequest("/api/v3/apiKey");
            request.AddHeader("x-api-sig", apiSig);
            request.AddParameter("accountId", accountId);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<ApiKeyResponse>(response.Content);
            return data;
        }

        public async Task<GetRelayerTimestampResponse> GetRelayerTimestamp()
        {

            var request = new RestRequest("/api/v3/timestamp");
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<GetRelayerTimestampResponse>(response.Content);
            return data;
        }

        public async Task<GetStorageIdResponse> GetStorageId(string apiKey, GetStorageIdRequest getStorageIdRequest)
        {
            var request = new RestRequest("/api/v3/storageId");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("accountId", getStorageIdRequest.AccountId);
            request.AddParameter("sellTokenId", getStorageIdRequest.SellTokenId);
            if(getStorageIdRequest.MaxNext.HasValue)
                request.AddParameter("maxNext", getStorageIdRequest.MaxNext.Value);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<GetStorageIdResponse>(response.Content);
            return data;
        }

        // minting
        public async Task<GetUserCollectionsResponse> GetNftCollection(string apiKey, GetUserCollectionRequest getUserCollectionRequest)
        {
            var request = new RestRequest("/api/v3/nft/collection");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("owner", getUserCollectionRequest.Owner);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<GetUserCollectionsResponse>(response.Content!);
            return data;
        }
        public async Task<GetOffchainFeeResponse> GetOffChainFee(string apiKey, GetOffchainFeeRequest getOffchainFeeRequest)
        {
            var request = new RestRequest("api/v3/user/nft/offchainFee");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("accountId", getOffchainFeeRequest.AccountId.ToString());
            request.AddParameter("requestType", getOffchainFeeRequest.RequestType.ToString());
            request.AddParameter("tokenAddress", getOffchainFeeRequest.TokenAddress);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<GetOffchainFeeResponse>(response.Content!);
            return data;
        }
        public async Task<PostMintNftResponse> PostMintNft(string apiKey, PostMintNftRequest postMintRequest)
        {
            var request = new RestRequest("api/v3/nft/mint");
            request.AddHeader("x-api-key", apiKey);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("exchange", postMintRequest.Exchange);
            request.AddParameter("minterId", postMintRequest.MinterId);
            request.AddParameter("minterAddress", postMintRequest.MinterAddress);
            request.AddParameter("toAccountId", postMintRequest.ToAccountId);
            request.AddParameter("toAddress", postMintRequest.ToAddress);
            request.AddParameter("nftType", postMintRequest.NftType);
            request.AddParameter("tokenAddress", postMintRequest.TokenAddress);
            request.AddParameter("nftId", postMintRequest.NftId);
            request.AddParameter("amount", postMintRequest.Amount.ToString());
            request.AddParameter("validUntil", postMintRequest.ValidUntil);
            request.AddParameter("royaltyPercentage", postMintRequest.RoyaltyPercentage);
            request.AddParameter("storageId", postMintRequest.StorageId);
            request.AddParameter("maxFee.tokenId", postMintRequest.MaxFee.TokenId);
            request.AddParameter("maxFee.amount", postMintRequest.MaxFee.Amount);
            request.AddParameter("forceToMint", "false");
            request.AddParameter("royaltyAddress", postMintRequest.RoyaltyAddress);
            request.AddParameter("counterFactualNftInfo.nftFactory", postMintRequest.CounterFactualNftInfo.nftFactory);
            request.AddParameter("counterFactualNftInfo.nftOwner", postMintRequest.CounterFactualNftInfo.nftOwner);
            request.AddParameter("counterFactualNftInfo.nftBaseUri", postMintRequest.CounterFactualNftInfo.nftBaseUri);
            request.AddParameter("eddsaSignature", postMintRequest.EddsaSignature);

            var response = await _client.ExecutePostAsync(request);
            var data = JsonConvert.DeserializeObject<PostMintNftResponse>(response.Content!);
            if(response.StatusDescription == "Bad Request")
            {
                Console.WriteLine($"Error Minting: {response.Content}");
                return null;
            }
            return data;
        }

    }
}
