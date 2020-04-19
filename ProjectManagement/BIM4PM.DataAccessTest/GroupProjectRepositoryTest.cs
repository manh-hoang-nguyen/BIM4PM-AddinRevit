namespace BIM4PM.DataAccessTest
{
    using BIM4PM.DataAccess;
    using BIM4PM.DataAccess.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GroupProjectRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {

            var authenticationRepository = new AuthenticationRepository(); 
            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }

        [TestMethod]
        public async Task GetGroupProjectsOfUser()
        {
            var groupProjectRepository = new GroupProjectRepository();
            var actual = await groupProjectRepository.GetGroupProjects();
            Assert.IsNotNull(actual);
        }
    }
}
