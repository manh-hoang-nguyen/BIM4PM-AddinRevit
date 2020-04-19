namespace BIM4PM.DataAccessTest
{
    using BIM4PM.DataAccess;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class UserRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {

            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }

        [TestMethod]
        public async Task GetMeAsync()
        {
            var userRepository = new UserRepository(); 

            var user = await userRepository.GetMeAsync();

            Assert.IsNotNull(user);
        }
    }
}
