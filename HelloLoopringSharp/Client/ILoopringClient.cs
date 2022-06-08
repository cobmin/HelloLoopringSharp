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
        Task<RelayerTimestamp> GetRelayerTimestamp();
    }
}
