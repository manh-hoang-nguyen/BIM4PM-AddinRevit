namespace BIM4PM.Model
{
    using Newtonsoft.Json;

    public class Project : EntityBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("groupProjectId")]
        public string GroupProjectId { get; set; }
    }
}
