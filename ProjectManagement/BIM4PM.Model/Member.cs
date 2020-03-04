using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
    public class Member
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("user")]
        public User user { get; set; }
        [JsonProperty("role")]
        public string role { get; set; }
    }
}
