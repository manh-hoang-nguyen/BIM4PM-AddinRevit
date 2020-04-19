namespace BIM4PM.Model
{
    using Newtonsoft.Json;

    public class GroupProject : EntityBase, IName, IDescription
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }
    }
}
