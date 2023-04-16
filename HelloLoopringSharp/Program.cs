using HelloLoopringSharp.ApiRequests;
using HelloLoopringSharp.ApiResponses;
using HelloLoopringSharp.Client;
using HelloLoopringSharp.Helpers;
using HelloLoopringSharp.Models;
using Microsoft.Extensions.Configuration;
using Nethereum.Signer;
using Newtonsoft.Json;

// minting: assumes you already have a collection made

var currentCid = "QmeFgyKBzBvfb9EitcksQMduho7RDXiJt2944pSJh32u6k";
var nftFactory = "0x0ad87482a1bfd0B3036Bb4b13708C88ACAe1b8bA"; // goerli
var exchange = "0x12b7cccF30ba360e5041C6Ce239C9a188B709b2B"; // goerli
var nftRoyaltyPercentage = 5; // 1% - 10%
var nftAmount = 100000; // amount should be  bigger than 0 and smaller than 100001
var maxFeeTokenId = 0; // 1 for LRC 0 for ETH
int nftType = 0; // 0 for 1155 1 for 721

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

ILoopringClient loopringClient = new LoopringClient();

GetAccountRequest getAccountRequest = new GetAccountRequest()
{
    AccountId = settings.AccountId,
    Owner = settings.Owner
};
var account = await loopringClient.GetAccount(getAccountRequest);

//Generate eddsaKeyPair from private key
var ethereumSigner = new EthereumMessageSigner();
var messageSignature = ethereumSigner.EncodeUTF8AndSign(account.KeySeed, new EthECKey(settings.MetamaskPrivateKey));
var layerTwoPrivateKey = SigningHelper.GetLayerTwoPrivateKeyFromLayerOnePrivateKey(messageSignature).secretKey;

var apiKey = await loopringClient.GetApiKey(layerTwoPrivateKey, account.AccountId);

// find collection you want to mint to
GetUserCollectionRequest getUserCollectionRequest = new GetUserCollectionRequest()
{
    Owner = account.Owner,
};
var userNftCollection = await loopringClient.GetNftCollection(apiKey.ApiKey, getUserCollectionRequest);
Console.WriteLine("Enter in the collection id you want to mint to.");
foreach (var user in userNftCollection.Collections)
    Console.WriteLine($"{user.Collection.Name}: {user.Collection.Id}");

var collectionIdResponse = Console.ReadLine();
var collectionAddress = userNftCollection.Collections.Where(x => x.Collection.Id == int.Parse(collectionIdResponse)).First();

// get collection address information
CounterFactualNftInfo counterFactualNftInfo = new CounterFactualNftInfo
{
    nftOwner = account.Owner,
    nftFactory = nftFactory,
    nftBaseUri = collectionAddress.Collection.BaseUri
};

// get storage id
GetStorageIdRequest getStorageIdRequest = new GetStorageIdRequest()
{
    AccountId = account.AccountId,
    SellTokenId = 0
};
var storageId = await loopringClient.GetStorageId(apiKey.ApiKey, getStorageIdRequest);

// get offchain fee
GetOffchainFeeRequest getOffchainFeeRequest = new GetOffchainFeeRequest()
{
    AccountId = account.AccountId,
    RequestType = 9,
    TokenAddress = collectionAddress.Collection.CollectionAddress
};
var offChainFee = await loopringClient.GetOffChainFee(apiKey.ApiKey, getOffchainFeeRequest);

// get valid until (current time + 2 months)
var relayerTime = await loopringClient.GetRelayerTimestamp();
long validUntil = TimeHelper.AddTwoMonthsToCurrentRelayerTime(relayerTime.Timestamp);

// get eddsa signature for mint 
EddsaSignatureForMintRequest eddsaSignatureForMintRequest = new()
{
    CurrentCid = currentCid,
    AccountId = account.AccountId,
    Owner = account.Owner,
    CollectionAddress = collectionAddress.Collection.CollectionAddress,
    NftRoyaltyPercentage = nftRoyaltyPercentage,
    Exchange = exchange,
    NftAmount = nftAmount,
    MaxFeeTokenId = maxFeeTokenId,
    Fee = offChainFee.Fees[maxFeeTokenId].Fee,
    ValidUntil = validUntil,
    OffchainId = storageId.OffchainId,
    LayerTwoPrivateKey = layerTwoPrivateKey
};
var eddsaSignatureForMintResponse = GenerateEddsaSignature.ForMinting(eddsaSignatureForMintRequest);

// post mint nft
PostMintNftRequest postMintNftRequest = new PostMintNftRequest()
{
    Exchange = exchange,
    MinterId = account.AccountId,
    MinterAddress = account.Owner,
    ToAccountId = account.AccountId,
    ToAddress = account.Owner,
    NftType = nftType,
    TokenAddress = collectionAddress.Collection.CollectionAddress,
    NftId = eddsaSignatureForMintResponse.nftId,
    Amount = nftAmount,
    ValidUntil = validUntil,
    RoyaltyPercentage = nftRoyaltyPercentage,
    RoyaltyAddress = account.Owner,
    StorageId = storageId.OffchainId,
    MaxFee = new MaxFee
    {
        TokenId = maxFeeTokenId,
        Amount = offChainFee.Fees[maxFeeTokenId].Fee,
    },
    ForceToMint = "false",
    CounterFactualNftInfo= counterFactualNftInfo,
    EddsaSignature = eddsaSignatureForMintResponse.eddsaSignature,
};

var mintNft = await loopringClient.PostMintNft(apiKey.ApiKey, postMintNftRequest);
Console.WriteLine($"Mint Response: {JsonConvert.SerializeObject(mintNft, Formatting.Indented)}");

