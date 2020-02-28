namespace BIM4PM.Model
{
    using System;
    using System.Collections.Generic;

    
    public class Project
    {
        public string _id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string owner { get; set; }

        public List<Member> members { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public int __v { get; set; }
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
