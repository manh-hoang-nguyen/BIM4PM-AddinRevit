using BIM4PM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIM4PM.DataAccessTests.Repositories
{
   
   public class AuthenticationRepositoryTest
    {
        [Theory]
        [InlineData(true,"nguyenhoang56ksgt@gmail.com","12345678")]
        [InlineData(false, "nguyenhoang56ksgt@gmail.com", "123456789")]
        public void Auth(bool expect,string email, string password)
        {
            var authenticationRepository = new AuthenticationRepository();

            var actual = authenticationRepository.Login(email, password);

            //Assert
            Assert.Equal(expect,actual); 
            if (expect == true) Assert.NotNull(AuthenticationRepository.Token);
            else Assert.Null(AuthenticationRepository.Token);
        }
    }
}
