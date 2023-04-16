using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelloLoopringSharp.ApiRequests
{
    public class GetStorageIdRequest
    {
        public int AccountId { get; set; }
        public int SellTokenId { get; set; }
        public bool? MaxNext { get; set; }
    }
}
