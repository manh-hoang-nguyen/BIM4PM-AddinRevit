namespace BIM4PM.Model
{
    using Newtonsoft.Json;
    using System;

    public abstract class EntityBase
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTime createdAt { get; set; }
        [JsonProperty("updatedAt")]
        public DateTime updatedAt { get; set; }
        [JsonProperty("__v")]
        public int __v { get; set; }
       
        public bool IsValid => Validate();
 
        public abstract bool Validate();
    }
}
