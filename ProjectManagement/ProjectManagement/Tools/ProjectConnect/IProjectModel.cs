using System.Collections.Generic;
using System.Threading.Tasks;
using BIM4PM.Model;

namespace BIM4PM.UI.Tools.Project
{
    public interface IProjectModel
    {
        void DocumentSet();
        Task<User> GetCurrentUser();
        void GetParamterElement();
        Task<IEnumerable<Model.Project>> GetProjectsOfUserAsync();
        Task<IEnumerable<ModelVersion>> GetVersionAsync(string modelId);
        Task<SynchroResponse> SendRevitElementToCloud(string modelid, SynchroBody body);
    }
}