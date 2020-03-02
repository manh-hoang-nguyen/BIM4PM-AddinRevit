namespace BIM4PM.Model
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
 
        public List<ProjectVersion> versions { get; set; } 
    }

    public class ProjectVersion:EntityBase
    { 
        public string description { get; set; }

        public string createdBy { get; set; }

        public int version { get; set; }

        public override bool Validate()
        {
            return true;
        }
    }
}
