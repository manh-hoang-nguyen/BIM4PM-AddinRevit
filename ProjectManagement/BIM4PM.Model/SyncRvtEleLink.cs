using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public class SyncRvtEleLink: EntityBase
    {
        [JsonProperty("RevitElementGuid")]
        public string revitElementGuid { get; set; }
        [JsonProperty("synchronisationId")]
        public string SynchronisationId { get; set; }
    }
}
