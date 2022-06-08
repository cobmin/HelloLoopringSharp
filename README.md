# HelloLoopringSharp
C# Wrapper around the Loopring API. Works with Metamask. Will work with WalletConnect eventually. 

You need to create an appsettings.json similar to below in the project directory with the setting "Copy to Output Directory" set to "Copy Always". Export out your Metamask private key and input into the appsettings.json file. DO NOT SHARE THIS PRIVATE KEY WITH ANYONE ELSE AT ALL.
```json
{
  "Settings": {
    "MetamaskPrivateKey": "blahblah" //Private key from metamask. DO NOT SHARE THIS ANYONE ELSE AT ALL.
  }
}
```

## Credits
Taranasus for the inspiration with LoopringSharp

Loopmon for the unity package which I based key pair generation from.
