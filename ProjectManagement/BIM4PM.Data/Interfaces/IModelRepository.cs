namespace BIM4PM.DataAccess.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelRepository
    {
        Task<IEnumerable<Model.Model>> GetModels(string projectId);
    }
}
