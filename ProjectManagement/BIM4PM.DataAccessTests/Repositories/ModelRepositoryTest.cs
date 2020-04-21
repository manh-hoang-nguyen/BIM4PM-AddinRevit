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
   public class ModelRepositoryTest
    {
        private string _projectId = "5e8062bfa6088610782f1a45";
        public ModelRepositoryTest()
        {
            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }

        [Fact]
        public async Task ShoudGetProjects()
        {
            var modelRepository = new ModelRepository();
            var actual = await modelRepository.GetModels(_projectId); 
            Assert.NotNull(actual.First().Id);
        }
    }
}
