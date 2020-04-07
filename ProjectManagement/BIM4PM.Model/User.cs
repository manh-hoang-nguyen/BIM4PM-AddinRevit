namespace BIM4PM.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class User : EntityBase
    { 
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }

        [EmailAddress]
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
    
}
