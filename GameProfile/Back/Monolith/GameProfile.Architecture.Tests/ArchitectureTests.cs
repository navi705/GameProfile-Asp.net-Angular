using NetArchTest.Rules;
using FluentAssertions;

namespace GameProfile.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "GameProfile.Domain";
        private const string ApplicationNamespace = "GameProfile.Application";
        private const string InfrastructureNamespace = "GameProfile.Infrastructure";
        private const string PersistenceNamespace = "GameProfile.Persistence";
        private const string WebApiNamespace = "GameProfile.WebApi";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assebmly = typeof(Domain.AssemblyReference).Assembly;

            var otherLayers = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PersistenceNamespace,
                WebApiNamespace
            };
            //Act
            var testResult = Types.InAssembly(assebmly).ShouldNot().HaveDependencyOnAll(otherLayers).GetResult();
            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }


        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assebmly = typeof(Application.AssemblyReference).Assembly;

            var otherLayers = new[]
            {
                InfrastructureNamespace,
                PersistenceNamespace,
                WebApiNamespace
            };
            //Act
            var testResult = Types.InAssembly(assebmly).ShouldNot().HaveDependencyOnAll(otherLayers).GetResult();
            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assebmly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherLayers = new[]
            {
                PersistenceNamespace,
                WebApiNamespace
            };
            //Act
            var testResult = Types.InAssembly(assebmly).ShouldNot().HaveDependencyOnAll(otherLayers).GetResult();
            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assebmly = typeof(Persistence.AssemblyReference).Assembly;

            var otherLayers = new[]
            {
                InfrastructureNamespace,
                WebApiNamespace
            };
            //Act
            var testResult = Types.InAssembly(assebmly).ShouldNot().HaveDependencyOnAll(otherLayers).GetResult();
            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

    }
}