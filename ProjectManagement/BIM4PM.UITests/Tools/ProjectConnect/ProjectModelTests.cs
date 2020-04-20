namespace BIM4PM.UITests.Tools.ProjectConnect
{
    using BIM4PM.DataAccess;
    using BIM4PM.DataAccess.Interfaces;
    using BIM4PM.Model;
    using BIM4PM.UI.Tools.Project;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ProjectModelTests
    {
        [Fact]
        public async Task ShouldLoadCurrentUser()
        {
            //Arrange
            var userMock = new Mock<IUserRepository>();
            userMock.Setup(user => user.GetMeAsync())
                .ReturnsAsync(new User
                {
                    Email = "nguyenhoang@gmail.com",
                    FirstName = "Nguyen",
                    LastName = "Manh Hoang"
                });

            var projectMock = new Mock<IProjectRepository>();
            projectMock.Setup(project => project.GetProjects())
                .ReturnsAsync(new List<Project>()
                {
                    new Project
                    {
                        Id ="1",
                        Name = "project1",
                        Description =" description project1",
                        Owner ="123",
                        GroupProjectId ="111"
                    },
                    new Project
                    {
                        Id ="2",
                        Name = "project2",
                        Description =" description project2",
                        Owner ="123",
                        GroupProjectId ="222"
                    }
                });
            var versionMock = new Mock<IVersionRepository>();
            versionMock.Setup(version => version.GetVersions("modelId"))
                .ReturnsAsync(new List<ModelVersion>
                {
                    new ModelVersion
                    {
                        Id="1",
                        Description= "version1",
                        Version="1",
                        ModelId="11"
                    },
                    new ModelVersion
                    {
                        Id="2",
                        Description= "version2",
                        Version="2",
                        ModelId="12"
                    },
                     new ModelVersion
                    {
                        Id="3",
                        Description= "version3",
                        Version="3",
                        ModelId="13"
                    },
                });

            var projectModel = new ProjectModel(userMock.Object, projectMock.Object, versionMock.Object);
            var currentUser = await projectModel.GetCurrentUser();
            Assert.Equal("nguyenhoang@gmail.com", currentUser.Email);
        }
    }
}
