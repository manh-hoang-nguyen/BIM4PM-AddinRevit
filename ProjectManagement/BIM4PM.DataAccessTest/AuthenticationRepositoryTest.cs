using System;
using System.Threading.Tasks;
using BIM4PM.DataAccess;
using BIM4PM.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIM4PM.DataAccessTest
{
    [TestClass]
    public class AuthenticationRepositoryTest
    {
        [TestMethod]
        public void Authenticated()
        {
            var authenticationRepository = new AuthenticationRepository(); 
            var actual = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
            bool IsAuthenticated = actual.Item1;
            Token token = actual.Item2;
            Assert.IsTrue(IsAuthenticated);
            Assert.IsNotNull(token);
        }
        [TestMethod]
        public void Unauthenticated()
        { 
            var authenticationRepository = new AuthenticationRepository();

            var actual = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "123456789");
            bool IsAuthenticated = actual.Item1;
            Token token = actual.Item2;
            Assert.IsFalse(IsAuthenticated);
            Assert.IsNull(token);
        }

        [TestMethod]
        public async Task AuthenticatedAsync()
        {
            
           // var authenticationRepository = new AuthenticationRepository();

           // var actual = await authenticationRepository.LoginAsync("nguyenhoang56ksgt@gmail.com", "12345678");

           //var  token = actual.Data as Token;

           // Assert.IsNotNull(token.token);
        }

    }
}
