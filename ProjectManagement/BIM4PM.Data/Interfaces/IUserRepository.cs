using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess.Interfaces
{
    interface IUserRepository 
    {
        User GetUser();
    }
}
