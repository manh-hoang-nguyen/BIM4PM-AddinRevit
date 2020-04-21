using BIM4PM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIM4PM.DataAccessTests.Repositories
{
   public class UserRepositoryTest
    {
        private string _userMail = "nguyenhoang56ksgt@gmail.com";
        public UserRepositoryTest()
        {
            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login(_userMail, "12345678");
        }
        [Fact]
        public async Task ShouldGetCurrentUser()
        {
            var userRepository = new UserRepository();

            var user = await userRepository.GetMeAsync();

            Assert.Equal(_userMail, user.Email);
        }
    }
}
