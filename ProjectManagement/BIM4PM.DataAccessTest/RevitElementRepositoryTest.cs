namespace BIM4PM.DataAccessTest
{
    using BIM4PM.DataAccess;
    using BIM4PM.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class RevitElementRepositoryTest
    {
        

        private List<RevitElement> _listRevitElements;

        /// <summary>
        /// The Initialize
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _listRevitElements = new List<RevitElement>()
            {
                new RevitElement(){},
                new RevitElement(){},
                new RevitElement(){},
            };
        }

        /// <summary>
        /// The GetAllElementsOfVersion
        /// </summary>
        [TestMethod]
        public void GetRevitElements()
        {
            var revitElementRepository = new RevitElementRepository();
            Project project = new Project
            {
                Id = "5d713995b721c3bb38c1f5d0"
            };
            ProjectVersion projectVersion = new ProjectVersion
            {
                version = 1
            };

            var result = revitElementRepository.GetRevitElements(project, projectVersion).ToList();

            var actual = result.Count > 0 ? true : false;

            Assert.AreEqual(true, actual);
        }
    }
}
