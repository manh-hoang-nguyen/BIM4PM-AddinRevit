using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
    public class User
    {
        public UserName name { get; set; }
        public string status { get; set; }
        public string _id { get; set; }
        public string email { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
        public List<Project> projects { get; set; }
    }
    public class UserName
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName()
        {
            return firstName + " " + lastName;
        }
    }
    public class UserRes
    {

        public bool success { get; set; }
        public User data { get; set; }

    }


 

}
