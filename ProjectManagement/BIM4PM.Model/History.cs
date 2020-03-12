using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
    public class History
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("modifiedAt")]
        public DateTime ModifiedAt { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("isFirstCommit")]
        public bool IsFirstCommit { get; set; }
        [JsonProperty("geometryChange")]
        public bool GeometryChange { get; set; }
        [JsonProperty("parameterChange")]
        public bool ParameterChange { get; set; }
        [JsonProperty("sharedParameterChange")]
        public bool SharedParameterChange { get; set; }
    }
}
