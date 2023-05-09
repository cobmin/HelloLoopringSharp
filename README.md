# Loopring Batch Mint NFTs
C# Wrapper around the Loopring API focused on batch minting NFTs. 
- Covers all the necessary API calls required to go from a Layer 1 API key to minting an NFT on Loopring.
- Works with GameStop and Metamask. You
- will need an active Loopring account, an NFT collection set up, and your JSON file CIDs ready for minting.

Also, before minting you need to modify the appsettings.json file in the project directory with the setting "Copy to Output Directory" set to "Copy Always". Export out your Metamask or GameStop private key and input that into the appsettings.json file along with your Loopring Account ID. DO NOT SHARE THIS PRIVATE KEY WITH ANYONE ELSE AT ALL.
```json
{
  "Settings": {
    "LayerOnePrivateKey": "6f87das6789fdsa6f87ads6dsf7878f9dssfd", //Private key from GameStop or MetaMask. DO NOT SHARE THIS PRIVATE KEY WITH ANYONE ELSE AT ALL."
    "AccountId": 12345, // loopring.io account id
    "Owner": "0x987d8saf7d8s9fads98fd7fds98fsd98d8s", // wallet address
    "Network": 5, // 1 = mainnet, 5 = goerli
    "maxFeeTokenId": 0, // 0 = ETH, 1 = LRC
    "nftType": 0, // 0 for 1155 1 for 721 (721 currently not supported)
    "nftRoyaltyPercentage": 5, // 1% - 10%
    "nftAmount": 100 // amount should be bigger than 0 and smaller than 100001
  }
}

```

## Credits
Fudgey for allowing the fork
