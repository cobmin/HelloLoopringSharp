# HelloLoopringSharp
C# Wrapper around the Loopring API. Works with GameStop and Metamask 

You need to create an appsettings.json file like below in the project directory with the setting "Copy to Output Directory" set to "Copy Always". Export out your Metamask private key and input that into the appsettings.json file. DO NOT SHARE THIS PRIVATE KEY WITH ANYONE ELSE AT ALL.
```json
{
  "Settings": {
    "MetamaskPrivateKey": "ebc2cd41b4b3fadsfadsfdsafadsfc55e885c4f0adsfasd", //Private key from GameStop or MetaMask. DO NOT SHARE THIS PRIVATE KEY WITH ANYONE ELSE AT ALL."
    "AccountId": 12345, // loopring.io account id
    "Owner": "0xfadsfadsfadsfadsfasdfadsasdfasd" // wallet address
  }
}

```

Currently Streamlining Minting

## Credits
Fudgey for allowing the fork
