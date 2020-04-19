namespace BIM4PM.Model
{
    using Newtonsoft.Json;

    public class ProjectAdmin : EntityBase
    {
        [JsonProperty("projectId")]
        public string ProjectId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
