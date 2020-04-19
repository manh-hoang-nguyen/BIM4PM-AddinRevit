using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public interface IRevitElementRepository
    {
        Task<IEnumerable<RevitElement>> GetAllElementsOfVersion(string versionId);
        Task<IEnumerable<RevitElement>> GetRevitElementsInPeriod(string versionId, string startDate, string endDate);
    }
}
