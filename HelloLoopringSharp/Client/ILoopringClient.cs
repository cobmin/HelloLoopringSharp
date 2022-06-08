using HelloLoopringSharp.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloLoopringSharp.Client
{
    public interface ILoopringClient
    {
        /// <summary>
        /// Gets the Loopring Relayer Timestamp
        /// </summary>
        /// <returns>Relayer Timestamp in milleseconds</returns>
        /// <exception cref="System.Exception">Thrown when there is an issue querying the Loopring API endpoint</exception>
        Task<RelayerTimestamp> GetRelayerTimestamp();

        /// <summary>
        /// Gets the Loopring Account Details
        /// </summary>
        /// <param name="owner">Loopring Hexadecimal Address</param>
        /// <param name="accountId">Loopring Account Id</param>
        /// <returns>Loopring Account Details</returns>
        /// <exception cref="System.Exception">Thrown when there is an issue querying the Loopring API endpoint</exception>
        Task<AccountDetails> GetAccount(string owner, int accountId);
    }
}
