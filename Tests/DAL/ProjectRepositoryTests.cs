using System;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Tests.DAL
{
    public class ProjectRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Logger<ProjectsRepository> _logger;
        public ProjectRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("D"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
            _logger = new Logger<ProjectsRepository>(new NullLoggerFactory());
        }
        
        [Fact]
        public async Task Create_ProjectShouldBeAdded_InProjectRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var project = new Project()
            {
                Name = "generic",
                ProjectStart = DateTime.Today,
                ProjectEnd = DateTime.Now
            };
            var repository = new ProjectsRepository(_logger, context);

            //Act
            var result = await repository.Create(project);
            
            //Assert
            result.Name.Should().Match(project.Name);
            result.Id.Should().NotBe(0);
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }
        
        [Fact]
        public async Task Update_UserShouldBeUpdated_InUserRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var project = new Project()
            {
                Name = "generic",
                ProjectStart = DateTime.Today,
                ProjectEnd = DateTime.Now
            };
            var repository = new ProjectsRepository(_logger, context);
            var toUpdate = new Project()
            {
                Name = "new generic",
                ProjectStart = DateTime.Today,
                ProjectEnd = DateTime.Now
            };

            //Act
            var result = await repository.Create(project);
            var updatedResult = await repository.Update(toUpdate);
            
            //Assert
            result.Name.Should().Match(project.Name);
            result.Id.Should().NotBe(0);
            updatedResult.Name.Should().NotMatch(result.Name);
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }
        
        [Fact]
        public async Task ReadAll_UserShouldBeFound_InUserRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var project = new Project()
            {
                Name = "generic",
                ProjectStart = DateTime.Today,
                ProjectEnd = DateTime.Now
            };
            var repository = new ProjectsRepository(_logger, context);

            //Act
            var result = await repository.Create(project);
            var userList = await repository.ReadAll();
            
            //Assert
            result.Name.Should().Match(project.Name);
            result.Id.Should().NotBe(0);
            userList.Should().Contain(e => e.Name == result.Name);
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }
    }
}
