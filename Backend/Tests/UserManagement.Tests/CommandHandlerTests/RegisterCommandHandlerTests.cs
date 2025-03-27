using MediatR;
using Modules.UserManagement.App.Commands.Register;
using Modules.UserManagement.App.Services.Password;
using Modules.UserManagement.Domain.Aggregates.Account;
using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;
using Modules.UserManagement.IntegrationEvents.Events;
using Moq;
using SharedUtils.Events;

namespace UserManagement.Tests.CommandHandlerTests;

public class RegisterCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepo;
    private readonly Mock<IPasswordService> _mockPasswordService;
    private readonly Mock<IEventsQueueService> _mockEventsQueue;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _mockAccountRepo = new Mock<IAccountRepository>();
        _mockPasswordService = new Mock<IPasswordService>();
        _mockEventsQueue = new Mock<IEventsQueueService>();

        _handler = new RegisterCommandHandler(
            _mockAccountRepo.Object,
            _mockPasswordService.Object,
            _mockEventsQueue.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldRegisterUser_AndPublishEvent()
    {
        // Arrange
        var request = new RegisterCommand("testuser", "test@example.com", "securepassword");

        var hashedPassword = "hashedpassword";

        _mockPasswordService.Setup(s => s.HashPassword(request.Password)).Returns(hashedPassword);
        _mockAccountRepo.Setup(r => r.AddAsync(It.IsAny<Account>())).Returns(Task.CompletedTask);
        _mockEventsQueue.Setup(e => e.Add(It.IsAny<UserRegisteredIntegrationEvent>()));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);

        _mockPasswordService.Verify(s => s.HashPassword(request.Password), Times.Once);
        _mockAccountRepo.Verify(r => r.AddAsync(It.Is<Account>(
            a => a.Username == request.Username &&
                 a.Email == request.Email &&
                 a.Password == hashedPassword
        )), Times.Once);

        _mockEventsQueue.Verify(e => e.Add(It.Is<UserRegisteredIntegrationEvent>(
            ev => ev.UserName == request.Username
        )), Times.Once);
    }
}