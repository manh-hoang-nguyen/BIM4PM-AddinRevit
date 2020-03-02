namespace BIM4PM.Model
{
    using System;
    using System.Collections.Generic;

    
    public class Project: EntityBase
    {
        

        public string name { get; set; }

        public string description { get; set; }

        public string owner { get; set; }

        public List<Member> members { get; set; }
         
       

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(_id)) isValid = false;

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
