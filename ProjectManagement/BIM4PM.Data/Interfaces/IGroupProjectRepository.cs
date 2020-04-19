using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIM4PM.Model;
namespace BIM4PM.DataAccess.Interfaces
{
   public interface IGroupProjectRepository
    {
        Task<IEnumerable<GroupProject>> GetGroupProjects();
    }
}
