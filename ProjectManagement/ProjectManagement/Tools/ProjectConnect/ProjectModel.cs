namespace BIM4PM.UI.Tools.Project
{
    using BIM4PM.DataAccess;
    using BIM4PM.DataAccess.Interfaces;
    using BIM4PM.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProjectModel : IProjectModel
    {
        IUserRepository _userRepository;

        IProjectRepository _projectRepository;

        IVersionRepository _versionRepository;

        public ProjectModel(
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IVersionRepository versionRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _versionRepository = versionRepository;
        }

        public async Task<User> GetCurrentUser()
        {
           
            var user = await _userRepository.GetMeAsync();
            return user;
        }

        public async Task<IEnumerable<Project>> GetProjectsOfUserAsync()
        {

            var projects = await _projectRepository.GetProjects();
            return projects;
        }

        public async Task<IEnumerable<ModelVersion>> GetVersionAsync(string modelId)
        { 
            var versions = await _versionRepository.GetVersions(modelId);
            return versions;
        }

        public void DocumentSet()
        {
            App.ModelHandler.Request.Make(RequestId.Model);
            App.ModelEvent.Raise();
        }

        public void GetParamterElement()
        {
            App.ModelHandler.Request.Make(RequestId.Element);
            App.ModelEvent.Raise();
        }

        public async Task<SynchroResponse> SendRevitElementToCloud(string modelid, SynchroBody body)
        {
            var synchroRepository = new SynchronizationRepository();
            var response = await synchroRepository.CreateSynchronization(modelid, body);

            return response;
        }
    }
}
