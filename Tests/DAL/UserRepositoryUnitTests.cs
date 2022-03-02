using System;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Tests.DAL
{
    public class UserRepositoryUnitTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Logger<UsersRepository> _logger;
        public UserRepositoryUnitTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("D"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
            _logger = new Logger<UsersRepository>(new NullLoggerFactory());
        }
        
        [Fact]
        public async Task Create_UserShouldBeAdded_InUserRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var user = new User()
            {
                Id = Guid.NewGuid().ToString("D"),
                FullName = "test name",
                Birthday = DateTime.Now
            };
            var repository = new UsersRepository(_logger, context);

            //Act
            var result = await repository.Create(user);
            
            //Assert
            result.FullName.Should().Match(user.FullName);
            result.Id.Should().NotBeNullOrEmpty();
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }
        
        [Fact]
        public async Task Update_UserShouldBeUpdated_InUserRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var user = new User()
            {
                Id = Guid.NewGuid().ToString("D"),
                FullName = "test name",
                Birthday = DateTime.Now
            };
            var repository = new UsersRepository(_logger, context);
            var toUpdate = new User()
            {
                Id = user.Id,
                FullName = "sec name",
                Birthday = DateTime.Now
            };

            //Act
            var result = await repository.Create(user);
            var updatedResult = await repository.Update(toUpdate);
            
            //Assert
            result.FullName.Should().Match(user.FullName);
            result.Id.Should().NotBeNullOrEmpty();
            updatedResult.FullName.Should().NotMatch(result.FullName);
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }
        
        [Fact]
        public async Task ReadAll_UserShouldBeFound_InUserRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var user = new User()
            {
                Id = Guid.NewGuid().ToString("D"),
                FullName = "test name",
                Birthday = DateTime.Now
            };
            var repository = new UsersRepository(_logger, context);

            //Act
            var result = await repository.Create(user);
            var userList = await repository.ReadAll();
            
            //Assert
            result.FullName.Should().Match(user.FullName);
            result.Id.Should().NotBeNullOrEmpty();
            userList.Should().Contain(e => e.Id == result.Id);
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }
    }
}
