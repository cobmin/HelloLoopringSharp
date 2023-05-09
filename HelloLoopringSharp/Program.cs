using HelloLoopringSharp.ApiRequests;
using HelloLoopringSharp.ApiResponses;
using HelloLoopringSharp.Client;
using HelloLoopringSharp.Helpers;
using HelloLoopringSharp.Models;
using Microsoft.Extensions.Configuration;
using Nethereum.Signer;
using Newtonsoft.Json;

// this minting assumes you already have a collection created. 

// input file for metadata cids
string input = ".\\input.txt";

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

// get mainnet or testnet information
var network = NetworkHelper.GetNetworkConfig(settings.Network);

ILoopringClient loopringClient = new LoopringClient(network.Url);

GetAccountRequest getAccountRequest = new GetAccountRequest()
{
    AccountId = settings.AccountId,
    Owner = settings.Owner
};

var account = await loopringClient.GetAccount(getAccountRequest);

// Generate eddsaKeyPair from private key
var ethereumSigner = new EthereumMessageSigner();
var messageSignature = ethereumSigner.EncodeUTF8AndSign(account.KeySeed, new EthECKey(settings.LayerOnePrivateKey));
var layerTwoPrivateKey = SigningHelper.GetLayerTwoPrivateKeyFromLayerOnePrivateKey(messageSignature).secretKey;

var apiKey = await loopringClient.GetApiKey(layerTwoPrivateKey, account.AccountId);

// find user collections 
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

using (StreamReader reader = new StreamReader(input))
{
    string currentCid;
    while ((currentCid = reader.ReadLine()) != null)
    {
        // get collection address information
        CounterFactualNftInfo counterFactualNftInfo = new CounterFactualNftInfo
        {
            nftOwner = account.Owner,
            nftFactory = network.NftFactory,
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
            NftRoyaltyPercentage = settings.NftRoyaltyPercentage,
            Exchange = network.Exchange,
            NftAmount = settings.NftAmount,
            MaxFeeTokenId = settings.MaxFeeTokenId,
            Fee = offChainFee.Fees[settings.MaxFeeTokenId].Fee,
            ValidUntil = validUntil,
            OffchainId = storageId.OffchainId,
            LayerTwoPrivateKey = layerTwoPrivateKey
        };
        var eddsaSignatureForMintResponse = GenerateEddsaSignature.ForMinting(eddsaSignatureForMintRequest);

        // post mint nft
        PostMintNftRequest postMintNftRequest = new PostMintNftRequest()
        {
            Exchange = network.Exchange,
            MinterId = account.AccountId,
            MinterAddress = account.Owner,
            ToAccountId = account.AccountId,
            ToAddress = account.Owner,
            NftType = settings.NftType,
            TokenAddress = collectionAddress.Collection.CollectionAddress,
            NftId = eddsaSignatureForMintResponse.nftId,
            Amount = settings.NftAmount,
            ValidUntil = validUntil,
            RoyaltyPercentage = settings.NftRoyaltyPercentage,
            RoyaltyAddress = account.Owner,
            StorageId = storageId.OffchainId,
            MaxFee = new MaxFee
            {
                TokenId = settings.MaxFeeTokenId,
                Amount = offChainFee.Fees[settings.MaxFeeTokenId].Fee,
            },
            ForceToMint = "false",
            CounterFactualNftInfo= counterFactualNftInfo,
            EddsaSignature = eddsaSignatureForMintResponse.eddsaSignature,
        };

        var mintNft = await loopringClient.PostMintNft(apiKey.ApiKey, postMintNftRequest);
        Console.WriteLine($"{currentCid} Mint Response: {JsonConvert.SerializeObject(mintNft, Formatting.Indented)}");


    }
}