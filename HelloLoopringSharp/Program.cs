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

var account = await loopringClient.GetAccount("0x36Cd6b3b9329c04df55d55D41C257a5fdD387ACd", 40940);
Console.WriteLine($"Account Details: {JsonConvert.SerializeObject(account, Formatting.Indented)}");

//Generate eddsaKeyPair
var ethereumSigner = new EthereumMessageSigner();
var messageSignature = ethereumSigner.EncodeUTF8AndSign(account.keySeed, new EthECKey(settings.MetamaskPrivateKey));
var eddsaKeyPair = PoseidonHelper.GetL2PKFromMetaMask(messageSignature);
