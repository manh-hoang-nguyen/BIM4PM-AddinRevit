using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public class SynchroBody
    {
        [JsonProperty("description")]
        public string Description { get; set; }

       [JsonProperty("data")]
        public List<RevitElement> Data { get; set; }
    }
}
