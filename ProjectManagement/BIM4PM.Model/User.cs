namespace BIM4PM.Model
{
    using System;
    using System.Collections.Generic;

    public class User : EntityBase
    {
        public UserName name { get; set; }

        public string status { get; set; }

        public string email { get; set; }

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
