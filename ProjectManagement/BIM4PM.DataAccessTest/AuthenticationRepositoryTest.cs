using System;
using BIM4PM.DataAccess;
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

            Assert.AreEqual(true, actual);
        }
        [TestMethod]
        public void Unauthenticated()
        {


            var authenticationRepository = new AuthenticationRepository();

            var actual = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "123456789");

            Assert.AreEqual(false, actual);
        }
    }
}
