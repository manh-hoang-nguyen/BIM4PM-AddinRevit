using BIM4PM.DataAccess;
using BIM4PM.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIM4PM.DataAccessTests.Repositories
{
   public class GroupProjectRepositoryTest
    {
        public GroupProjectRepositoryTest()
        {
            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }
        [Fact]
        public async Task GetGroupProjectsOfUser()
        {
            var groupProjectRepository = new GroupProjectRepository();
            var actual = await groupProjectRepository.GetGroupProjects();
            Assert.NotNull(actual.First().Id);
        }
    }
}
