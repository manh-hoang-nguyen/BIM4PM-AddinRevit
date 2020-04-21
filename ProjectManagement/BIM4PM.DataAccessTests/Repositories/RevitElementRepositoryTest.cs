using BIM4PM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BIM4PM.DataAccessTests.Repositories
{
   public class RevitElementRepositoryTest
    {
        private string _versionId = "5e80834c3952c3079084f77c";

        public RevitElementRepositoryTest()
        {
            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }
        [Fact]
        public async Task GetRevitElements()
        {
            var revitElementRepository = new RevitElementRepository(); 
            var actual = await revitElementRepository.GetAllElementsOfVersion(_versionId); 
            Assert.NotNull(actual.First().Id);
        }

        [Theory] 
        [InlineData(null, "2020-04-17", "2020-04-18")] 
        public async Task GetRevitElementInPeriodSuccess(object expect, string startDate, string endDate)
        {  
            var revitElementRepository = new RevitElementRepository();
            var actual = await revitElementRepository.GetRevitElementsInPeriod(_versionId, startDate, endDate); 
            Assert.NotEqual(expect, actual);
        }

        [Fact]
        public async Task NotAllowSameDate_GetRevitElementInPeriod()
        { 
            string startDate = "2020-04-17";
            string endDate = "2020-04-17"; 
            var revitElementRepository = new RevitElementRepository();
            Func<Task> actual = () => revitElementRepository.GetRevitElementsInPeriod(_versionId, startDate, endDate);

            //Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(actual);
            Assert.Contains("Bad Request", ex.Message);
        }

        [Theory]
        [InlineData("2020-04-17", "2020/04/18")]
        [InlineData("17-04-2020", "2020-04-18")]
        [InlineData("2020-17-04", "2020-04-18")]
        public async Task IncorrectFormat_GetRevitElementInPeriod(string startDate, string endDate )
        { 
            var revitElementRepository = new RevitElementRepository();
            Func<Task> actual = () => revitElementRepository.GetRevitElementsInPeriod(_versionId, startDate, endDate);
            FormatException ex = await Assert.ThrowsAsync<FormatException>(actual);
            Assert.Contains("Bad format of startDate or endDate.", ex.Message);
        }
    }
}
