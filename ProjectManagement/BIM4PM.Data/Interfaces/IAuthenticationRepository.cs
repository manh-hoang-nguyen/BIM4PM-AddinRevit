using BIM4PM.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
    public interface IAuthenticationRepository
    {
        bool Login(string email, string password);
     
        void Logout();
         
    }
}
