using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloLoopringSharp.ApiRequests
{
    public class GetAccountRequest
    {
        public int accountId { get; set; }
        public string owner { get; set; }
    }
}
