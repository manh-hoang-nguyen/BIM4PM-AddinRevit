namespace BIM4PM.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Modifications
    {
        [JsonProperty("parameters")]
        public List<string> Parameters { get; set; }

        [JsonProperty("sharedParameters")]
        public List<string> SharedParameters { get; set; }

        [JsonProperty("geometryParameters")]
        public List<string> GeometryParameters { get; set; }
    }
}
