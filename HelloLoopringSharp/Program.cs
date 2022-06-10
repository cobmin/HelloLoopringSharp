// See https://aka.ms/new-console-template for more information
using HelloLoopringSharp.ApiRequests;
using HelloLoopringSharp.Client;
using HelloLoopringSharp.Helpers;
using Microsoft.Extensions.Configuration;
using Nethereum.Signer;
using Newtonsoft.Json;
using PoseidonSharp;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

ILoopringClient loopringClient = new LoopringClient();

//Get relayer timestamp
var timestamp = await loopringClient.GetRelayerTimestamp();
Console.WriteLine($"Relayer Timestamp: {JsonConvert.SerializeObject(timestamp, Formatting.Indented)}");

GetAccountRequest getAccountRequest = new GetAccountRequest()
{
    accountId = 136736,
    owner = "0x991B6fE54d46e5e0CEEd38911cD4a8694bed386A"
};
var account = await loopringClient.GetAccount(getAccountRequest);
Console.WriteLine($"Account Details: {JsonConvert.SerializeObject(account, Formatting.Indented)}");

//Generate eddsaKeyPair from Metamask private key
var ethereumSigner = new EthereumMessageSigner();
var messageSignature = ethereumSigner.EncodeUTF8AndSign(account.keySeed, new EthECKey(settings.MetamaskPrivateKey));
var layerTwoPrivateKey = EcdsaSigningHelper.GetLayerTwoPrivateKeyFromLayerOnePrivateKey(messageSignature).secretKey;

var apiKey = await loopringClient.GetApiKey(layerTwoPrivateKey, account.accountId);
Console.WriteLine($"Api Key: {JsonConvert.SerializeObject(apiKey, Formatting.Indented)}");

UpdateApiKeyRequest updateApiKeyRequest = new UpdateApiKeyRequest()
{
    accountId = 136736
};
var apiKey2 = await loopringClient.UpdateApiKey(layerTwoPrivateKey, apiKey.apiKey, updateApiKeyRequest);
Console.WriteLine($"Updated Api Key: {JsonConvert.SerializeObject(apiKey2, Formatting.Indented)}");

