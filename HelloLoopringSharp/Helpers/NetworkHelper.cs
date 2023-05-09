namespace LoopringBatchMintNFTs.Helpers
{
    public class NetworkHelper
    {
        public string NftFactory { get; set; }
        public string Exchange { get; set; }
        public string Url { get; set; }

        public static NetworkHelper GetNetworkConfig(int variable)
        {
            if (variable == 1)
            {
                return new NetworkHelper
                {
                    NftFactory = "0xc852aC7aAe4b0f0a0Deb9e8A391ebA2047d80026",
                    Exchange = "0x0BABA1Ad5bE3a5C0a66E7ac838a129Bf948f1eA4",
                    Url = "https://api3.loopring.io"
                };
            }
            else if (variable == 5)
            {
                return new NetworkHelper
                {
                    NftFactory = "0x0ad87482a1bfd0B3036Bb4b13708C88ACAe1b8bA",
                    Exchange = "0x12b7cccF30ba360e5041C6Ce239C9a188B709b2B",
                    Url = "https://uat2.loopring.io"
                };
            }
            else
            {
                throw new ArgumentException("Invalid variable value.");
            }
        }
    }
}
