using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeConverter.Models
{
    class MojangShapedModel
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string group { get; set; }
        [JsonProperty("pattern", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> pattern { get; set; }
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dictionary<string, string>> keys = new Dictionary<string, Dictionary<string, string>>();
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public MojangItem result { get; set; }


    }

    class MojangItem {
        [JsonProperty("item", NullValueHandling = NullValueHandling.Ignore)]
        public string item { get; set; }
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int count { get; set; }
    }
}
