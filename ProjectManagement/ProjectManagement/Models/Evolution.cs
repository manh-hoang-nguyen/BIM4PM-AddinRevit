using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Evolution
    {
        public string _id { get; set; }
        public string guid { get; set; }
        public Comment[] comments { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updateAt { get; set; }
        public int __v { get; set; }
    }

    public class Comment
    {
        public string _id { get; set; }
        public string v { get; set; }
        public string auteur { get; set; }
        public string content { get; set; }
        public DateTime datetime { get; set; }
    }
}
