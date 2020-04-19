using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public interface ISynchronizationRepository
    {
        Task<SynchroResponse> CreateSynchronization(string modelId, SynchroBody body);
        Task<IEnumerable<Synchronization>> GetSynchronizations(string modelId);
        Task<Synchronization> GetLastSynchronization(string modelId);
    }
}
