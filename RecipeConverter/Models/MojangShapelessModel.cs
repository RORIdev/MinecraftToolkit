using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeConverter.Models
{
    class MojangShapelessModel
    {
         [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }
         [JsonProperty("ingredients", NullValueHandling = NullValueHandling.Ignore)]
        public List<MojangItem> ingredients { get; set; }   
         [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public MojangItem result { get; set; }
    }
}
