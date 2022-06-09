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

        public async Task<AccountDetails> GetAccount(string owner, int accountId)
        {
            var request = new RestRequest("/api/v3/account");
            request.AddParameter("owner", owner);
            request.AddParameter("accountId", accountId);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<AccountDetails>(response.Content);
            return data;
        }

        public async Task<ApiKey> GetApiKey(string layerTwoPrivateKey, int accountId)
        {
            var request = new RestRequest("/api/v3/apiKey");
            var apiSig = UrlHelper.Sign(
               layerTwoPrivateKey,
              HttpMethod.Get,
              new List<(string Key, string Value)>() { ("accountId", accountId.ToString()) },
              null,
              "/api/v3/apiKey",
              _baseUrl);
            request.AddHeader("x-api-sig", apiSig);
            request.AddParameter("accountId", accountId);
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<ApiKey>(response.Content);
            return data;
        }

        public async Task<string> UpdateApiKey(string layerTwoPrivateKey, int accountId, string apiKey)
        {
            string requestBody = "{\"accountId\":" + accountId + "}";
            var request = new RestRequest("/api/v3/apiKey", Method.Post);
            var apiSig = UrlHelper.Sign(
               layerTwoPrivateKey,
              HttpMethod.Post,
              null,
              requestBody,
              "/api/v3/apiKey",
              _baseUrl);
            request.AddHeader("X-API-KEY", apiKey);
            request.AddHeader("X-API-SIG", apiSig);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("accountId", accountId);
            var response = await _client.ExecuteAsync(request);
            var data = response.Content;
            return data;
        }

        public async Task<RelayerTimestamp> GetRelayerTimestamp()
        {

            var request = new RestRequest("/api/v3/timestamp");
            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<RelayerTimestamp>(response.Content);
            return data;
        }
    }
}
