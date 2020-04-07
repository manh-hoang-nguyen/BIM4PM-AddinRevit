using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public class Model: EntityBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("discipline")]
        public string Discipline { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
