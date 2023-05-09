using JsonFlatten;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopringBatchMintNFTs.Helpers
{
    public static class JsonFlattenHelper
    {
        public static string Flatten(object jsonObject)
        {
            var jObject = JObject.Parse(JsonConvert.SerializeObject(jsonObject));
            var jObjectFlattened = jObject.Flatten();
            var jObjectFlattenedString = JsonConvert.SerializeObject(jObjectFlattened);
            return jObjectFlattenedString;
        }
    }
}
