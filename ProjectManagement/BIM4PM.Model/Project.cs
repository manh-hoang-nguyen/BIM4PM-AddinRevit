namespace BIM4PM.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    
    public class Project: EntityBase
    {
        public Project()
        {
            Members = new List<Member>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("members")]
        public List<Member> Members { get; set; }
         
       

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(Id)) isValid = false;

            return isValid;
        }
    }

    public class ProjectRes
    {
        public bool success { get; set; }

        public List<Project> data { get; set; }
    }
    public class SingleProjectRes
    {
        public bool success { get; set; }

        public Project data { get; set; }
    }
}
