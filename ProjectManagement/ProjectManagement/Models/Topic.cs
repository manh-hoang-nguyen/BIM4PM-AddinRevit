using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Topic
    {
        public string _id { get; set; }

        public string project { get; set; }

        public string degree { get; set; }

        public List<string> elements { get; set; }

        public User user { get; set; }

        public List<ChildComment> comments { get;set;}
    }
}
