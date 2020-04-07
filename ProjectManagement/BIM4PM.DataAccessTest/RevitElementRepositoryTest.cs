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
           
        }
    }
}
