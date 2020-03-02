using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
    public interface IAuthenticationRepository
    {
        string Token { get; set; }
        bool IsAuthenticated { get; set; }
    }
}
