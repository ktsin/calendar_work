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
    public class MessagesRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Logger<MessagesRepository> _logger;
        public MessagesRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("D"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;
            _logger = new Logger<MessagesRepository>(new NullLoggerFactory());
        }
        
        [Fact]
        public async Task Create_MessageShouldBeAdded_InMessagesRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var message = new Message()
            {
                Id = Guid.NewGuid().ToString(),
                MessageBody = "MessagesRepositoryTests"
            };
            
            var repository = new MessagesRepository(_logger, context);

            //Act
            var result = await repository.Create(message);
            
            //Assert
            result.MessageBody.Should().Match(message.MessageBody);
            result.Id.Should().NotBeNullOrEmpty();
            
            //Clean
            await repository.Delete(result.Id);
            (await repository.ReadAll()).Should().NotContain(e => e.Id == result.Id);
        }

        [Fact]
        public async Task ReadAll_MessageShouldBeFound_InMessagesRepository()
        {
            //arrange
            await using var context = new DataContext(_options);
            var user = new User()
            {
                Id = Guid.NewGuid().ToString("D"),
                FullName = "test name",
                Birthday = DateTime.Now
            };
            var repository = new UsersRepository(null, context);

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
