using HelloLoopringSharp.ApiRequests;
using HelloLoopringSharp.ApiResponses;
using HelloLoopringSharp.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloLoopringSharp.Client
{
    public class LoopringClient : ILoopringClient, IDisposable
    {
        const string _baseUrl = "https://api3.loopring.io";

        readonly RestClient _client;

        public LoopringClient()
        {
            _client = new RestClient(_baseUrl);
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<AccountDetailsResponse> GetAccount(GetAccountRequest getAccountRequest)
        {
            var request = new RestRequest("/api/v3/account");
            request.AddParameter("owner", getAccountRequest.owner);
            request.AddParameter("accountId", getAccountRequest.accountId);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<AccountDetailsResponse>(response.Content);
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

        public async Task<ApiKeyResponse> UpdateApiKey(string layerTwoPrivateKey, string apiKey, UpdateApiKeyRequest updateApiKeyRequest)
        {
            string requestBody = JsonFlattenHelper.Flatten(updateApiKeyRequest);
            
            var apiSig = UrlHelper.Sign(
               layerTwoPrivateKey,
              HttpMethod.Post,
              null,
              requestBody,
              "/api/v3/apiKey",
              _baseUrl);
            var request = new RestRequest("/api/v3/apiKey", Method.Post);
            request.AddHeader("x-api-key", apiKey);
            request.AddHeader("x-api-sig", apiSig);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            var response = await _client.ExecuteAsync(request);
            var data = JsonConvert.DeserializeObject<ApiKeyResponse>(response.Content);
            return data;
        }

        public async Task<RelayerTimestampResponse> GetRelayerTimestamp()
        {

            var request = new RestRequest("/api/v3/timestamp");
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<RelayerTimestampResponse>(response.Content);
            return data;
        }
    }
}
