using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloLoopringSharp.ApiRequests
{
    public class GetStorageIdRequest
    {
        public int accountId { get; set; }
        public int sellTokenId { get; set; }
        public bool? maxNext { get; set; }
    }
}
