namespace BIM4PM.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class User : EntityBase
    {
        [JsonProperty("name")]
        public UserName name { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("projects")]
        public List<Project> projects { get; set; }

        /// <summary>
        /// The Validate
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }

    public class UserName
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        /// <summary>
        /// The fullName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string fullName
        {
            get
            {
                return firstName + " " + lastName;
            }
           
        }
    }

    public class UserRes
    {
        public bool success { get; set; }

        public User data { get; set; }
    }
}
