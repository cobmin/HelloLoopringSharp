using HelloLoopringSharp.Models;
using Multiformats.Hash;
using PoseidonSharp;
using System.Numerics;

namespace HelloLoopringSharp.Helpers
{
    public class GenerateEddsaSignature
    {
        public static (string nftId, string eddsaSignature) ForMinting(EddsaSignatureForMintRequest request)
        {
            //Generate the nft id here
            Multihash multiHash = Multihash.Parse(request.CurrentCid, Multiformats.Base.MultibaseEncoding.Base58Btc);
            string multiHashString = multiHash.ToString();
            var ipfsCidBigInteger = SigningHelper.ParseHexUnsigned(multiHashString);
            var nftId = "0x" + ipfsCidBigInteger.ToString("x").Substring(4);

            //Generate the poseidon hash for the nft data
            var nftIdHi = SigningHelper.ParseHexUnsigned(nftId.Substring(0, 34));
            var nftIdLo = SigningHelper.ParseHexUnsigned(nftId.Substring(34, 32));
            BigInteger[] nftDataPoseidonInputs =
            {
                SigningHelper.ParseHexUnsigned(request.Owner),
                (BigInteger) 0,
                SigningHelper.ParseHexUnsigned(request.CollectionAddress),
                nftIdLo,
                nftIdHi,
                (BigInteger)request.NftRoyaltyPercentage
            };
            Poseidon nftDataPoseidon = new Poseidon(7, 6, 52, "poseidon", 5, _securityTarget: 128);
            BigInteger nftDataPoseidonHash = nftDataPoseidon.CalculatePoseidonHash(nftDataPoseidonInputs);

            //Generate the poseidon hash for the remaining data
            BigInteger[] nftPoseidonInputs =
            {
                SigningHelper.ParseHexUnsigned(request.Exchange),
                (BigInteger) request.AccountId,
                (BigInteger) request.AccountId,
                nftDataPoseidonHash,
                (BigInteger) request.NftAmount,
                (BigInteger) request.MaxFeeTokenId,
                BigInteger.Parse(request.Fee),
                (BigInteger) request.ValidUntil,
                (BigInteger) request.OffchainId
            };
            Poseidon nftPoseidon = new Poseidon(10, 6, 53, "poseidon", 5, _securityTarget: 128);
            BigInteger nftPoseidonHash = nftPoseidon.CalculatePoseidonHash(nftPoseidonInputs);

            //Generate the poseidon eddsa signature
            Eddsa eddsa = new Eddsa(nftPoseidonHash, request.LayerTwoPrivateKey);
            string eddsaSignature = eddsa.Sign();

            return (nftId, eddsaSignature);
        }
    }
}
