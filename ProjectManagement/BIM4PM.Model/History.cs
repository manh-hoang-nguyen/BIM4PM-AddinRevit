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
        public string _id { get; set; }
        [JsonProperty("modifiedAt")]
        public DateTime modifiedAt { get; set; }
        [JsonProperty("user")]
        public User user { get; set; }
        [JsonProperty("isFirstCommit")]
        public bool isFirstCommit { get; set; }
        [JsonProperty("geometryChange")]
        public bool geometryChange { get; set; }
        [JsonProperty("parameterChange")]
        public bool parameterChange { get; set; }
        [JsonProperty("sharedParameterChange")]
        public bool sharedParameterChange { get; set; }
    }
}
