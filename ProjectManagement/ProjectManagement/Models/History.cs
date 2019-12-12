using System;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
    public class Historyx
    {
        public string _id { get; set; }
        public string projectId { get; set; }
        public string guid { get; set; }
        public string status { get; set; }
        public Modification[] modifications { get; set; }
        public ChildComment[] comments { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
    }

    public class Modification
    {
        public string _id { get; set; }
        public int v { get; set; }
        public string auteur { get; set; }
        public string comment { get; set; }
        public DateTime datetime { get; set; }
    }

   
  
}