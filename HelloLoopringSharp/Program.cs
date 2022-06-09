// See https://aka.ms/new-console-template for more information
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

var account = await loopringClient.GetAccount("0x991B6fE54d46e5e0CEEd38911cD4a8694bed386A", 136736);
Console.WriteLine($"Account Details: {JsonConvert.SerializeObject(account, Formatting.Indented)}");

//Generate eddsaKeyPair from Metamask private key
var ethereumSigner = new EthereumMessageSigner();
var messageSignature = ethereumSigner.EncodeUTF8AndSign(account.keySeed, new EthECKey(settings.MetamaskPrivateKey));
var layerTwoPrivateKey = EcdsaSigningHelper.GetLayerTwoPrivateKeyFromLayerOnePrivateKey(messageSignature).secretKey;

var apiKey = await loopringClient.GetApiKey(layerTwoPrivateKey, account.accountId);
Console.WriteLine($"Api Key: {JsonConvert.SerializeObject(apiKey, Formatting.Indented)}");

var apiKey2 = await loopringClient.UpdateApiKey(layerTwoPrivateKey, account.accountId, apiKey.apiKey);
Console.WriteLine($"Updated Api Key: {JsonConvert.SerializeObject(apiKey2, Formatting.Indented)}");

