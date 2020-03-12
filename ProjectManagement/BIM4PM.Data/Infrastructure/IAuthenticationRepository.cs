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
        Task<IRestResponse<Token>> LoginAsync(string email, string password);
        Task LogoutAsync();
        bool IsAuthenticated { get; set; }
    }
}
