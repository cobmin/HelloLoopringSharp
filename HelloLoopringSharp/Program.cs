// See https://aka.ms/new-console-template for more information
using HelloLoopringSharp.Client;
using Newtonsoft.Json;

ILoopringClient loopringClient = new LoopringClient();

//Get relayer timestamp
var timestamp = await loopringClient.GetRelayerTimestamp();
Console.WriteLine($"Relayer Timestamp: {JsonConvert.SerializeObject(timestamp, Formatting.Indented)}");

var account = await loopringClient.GetAccount("0x36Cd6b3b9329c04df55d55D41C257a5fdD387ACd", 40940);
Console.WriteLine($"Account Details: {JsonConvert.SerializeObject(account, Formatting.Indented)}");

