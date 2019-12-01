namespace ProjectManagement.Models
{
    using System;
    using System.Collections.Generic;

    public class RevitElementRes
    {
        public bool success { get; set; }
        public List<RevitElement> data { get; set; }
    }
    public class RevitElement
    {
        public string _id { get; set; }

        public string project { get; set; }

        public string version { get; set; }

        public string guid { get; set; }

        public string name { get; set; }

        public int elementId { get; set; }

        public string category { get; set; }

        public string level { get; set; }

        public string parameters { get; set; }

        public string sharedParameters { get; set; }

        public string worksetId { get; set; }

        public string location { get; set; }

        public string boundingBox { get; set; }

        public string centroid { get; set; }

        public string typeId { get; set; }

        public string volume { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public int __v { get; set; }
    }
}
