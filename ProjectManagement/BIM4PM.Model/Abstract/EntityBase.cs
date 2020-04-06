namespace BIM4PM.Model
{
    using Newtonsoft.Json;
    using System;

    public abstract class EntityBase
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("__v")]
        public int V { get; set; } 
    }
}
