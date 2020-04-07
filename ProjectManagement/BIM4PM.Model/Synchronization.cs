using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public class Synchronization: EntityBase
    {
        [JsonProperty("modelId")]
        public string ModelId { get; set; }
        [JsonProperty("versionId")]
        public string VersionId { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
