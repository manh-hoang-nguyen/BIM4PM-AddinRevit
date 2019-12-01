using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Routes
{
    public class DataRouter
    {
        public static string GetData = "https://my-first-web-1nodejs.herokuapp.com/data";
        public static string PostData = "https://my-first-web-1nodejs.herokuapp.com/data/add";
        public static string PostMultipleData = "http://my-first-web-1nodejs.herokuapp.com/data/addMultiple";
        public static string StatusChange = "https://my-first-web-1nodejs.herokuapp.com/data/statusChange";
    }
}
