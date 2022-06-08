// See https://aka.ms/new-console-template for more information
using HelloLoopringSharp.Client;
using HelloLoopringSharp.Helpers;
using Microsoft.Extensions.Configuration;
using Nethereum.Signer;
using Newtonsoft.Json;
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

//Testing signer
var ethereumSigner = new EthereumMessageSigner();
var messageSignature = ethereumSigner.EncodeUTF8AndSign(account.keySeed, new EthECKey(settings.MetamaskPrivateKey));
Console.WriteLine(messageSignature);

byte[] requestBytes = Encoding.UTF8.GetBytes(messageSignature);
SHA256Managed sha256Managed = new SHA256Managed();
byte[] sha256HashBytes = sha256Managed.ComputeHash(requestBytes);
string sha256HashString = string.Empty;
foreach (byte x in sha256HashBytes)
{
    sha256HashString += String.Format("{0:x2}", x);
}

BigInteger sha256HashNumber = BigInteger.Parse(sha256HashString, NumberStyles.AllowHexSpecifier);
if (sha256HashNumber.Sign == -1)
{
    string sha256HashAsPositiveHexString = "0" + sha256HashString;
    sha256HashNumber = BigInteger.Parse(sha256HashAsPositiveHexString, NumberStyles.AllowHexSpecifier);
}