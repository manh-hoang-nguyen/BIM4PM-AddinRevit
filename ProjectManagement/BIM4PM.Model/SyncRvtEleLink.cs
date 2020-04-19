namespace BIM4PM.Model
{
    using Newtonsoft.Json;

    public class SyncRvtEleLink : EntityBase
    {
        [JsonProperty("revitElementId")]
        public string RevitElementId { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("synchronisationId")]
        public string SynchronisationId { get; set; }
    }
}
