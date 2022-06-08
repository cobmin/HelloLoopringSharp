// See https://aka.ms/new-console-template for more information
using HelloLoopringSharp.Client;
using Newtonsoft.Json;

ILoopringClient loopringClient = new LoopringClient();
var timestamp = await loopringClient.GetRelayerTimestamp();
Console.WriteLine(JsonConvert.SerializeObject(timestamp, Formatting.Indented));
