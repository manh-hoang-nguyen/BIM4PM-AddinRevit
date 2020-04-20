using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public class ModelVersion: EntityBase
    {
        [JsonProperty("modelId")]
        public string ModelId { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
