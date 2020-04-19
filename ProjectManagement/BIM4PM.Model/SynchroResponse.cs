using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public class SynchroResponse
    {
        [JsonProperty("synchronization")]
        public Synchronization Synchronization { get; set; }
        [JsonProperty("revitelements")]
        public List<RevitElement> RevitElements { get; set; }
    }
}
