namespace BIM4PM.DataAccessTest
{
    using BIM4PM.DataAccess;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class AuthenticationRepositoryTest
    {
        [TestMethod]
        public void UnauthenticatedAsync()
        {
            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "123456789");


            Assert.IsFalse(isAuth);
        }

        [TestMethod]
        public void AuthenticatedAsync()
        {

            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");

            Assert.IsTrue(isAuth);
        }

       
    }
}
