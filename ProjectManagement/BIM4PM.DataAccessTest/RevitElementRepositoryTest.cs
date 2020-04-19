namespace BIM4PM.DataAccessTest
{
    using BIM4PM.DataAccess;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class RevitElementRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {

            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }

        [TestMethod]
        public async Task GetRevitElements()
        {
            string versionId = "5e80834c3952c3079084f77c";
            var revitElementRepository = new RevitElementRepository();
            var actual = await revitElementRepository.GetAllElementsOfVersion(versionId);

            Assert.IsNotNull(actual.ToArray());
        }

        [TestMethod]
        public async Task GetRevitElementInPeriodSuccess()
        {
            string versionId = "5e80834c3952c3079084f77c";
            string startDate = "2020-04-17";
            string endDate = "2020-04-18";

            var revitElementRepository = new RevitElementRepository();
            var actual = await revitElementRepository.GetRevitElementsInPeriod(versionId, startDate, endDate);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetRevitElementInPeriodFail()
        {
            string versionId = "5e80834c3952c3079084f77c";
            string startDate = "2020-04-17";
            string endDate = "2020-04-17";

            var revitElementRepository = new RevitElementRepository();
            var actual = await revitElementRepository.GetRevitElementsInPeriod(versionId, startDate, endDate);
        }
    }
}
