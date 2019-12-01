namespace ProjectManagement.Models
{
    using System;
    using System.Collections.Generic;

    public class VersionRes
    {
        public bool success { get; set; }

        public List<VersionParent> data { get; set; }
    }

    public class VersionParent
    {
        public string _id { get; set; }
 
        public List<Version> versions { get; set; }

      
    }

    public class Version
    {
        public string _id { get; set; }

        public DateTime createdAt { get; set; }

        public string description { get; set; }

        public string createdBy { get; set; }

        public int version { get; set; }
    }
}
