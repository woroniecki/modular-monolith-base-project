using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Modules.UserManagement.App.Commands.Login;
using Modules.UserManagement.Domain.Aggregates;
using Modules.UserManagement.Infrastructure.DataAccessLayer.Repositories;
using Moq;
using SharedUtils.Jwt;
using UserAuth.App.Services.Password;

namespace UserManagement.Tests.CommandHandlerTests;

public class LoginCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _mockAccountRepo;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Salt", "test-salt-value" },
            { "JwtSettings:SecretKey", "testestestestestverysecuresecretkey1234567890" },
            { "JwtSettings:ExpirationInMinutes", "60" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _mockAccountRepo = new Mock<IAccountRepository>();
        _passwordService = new PasswordService(configuration);
        _jwtService = new JwtService(configuration);

        _handler = new LoginCommandHandler(
            _mockAccountRepo.Object,
            _passwordService,
            _jwtService
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnJwtToken_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new LoginCommand("testuser", "password123");
        var jwtToken = "valid-jwt-token";
        var hashedPassword = _passwordService.HashPassword(request.Password);

        var account = new Account("testuser", "test@example.com", hashedPassword);

        _mockAccountRepo.Setup(r => r.GetByUsernameAsync(request.Username)).ReturnsAsync(account);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.accessToken);

        Assert.True(new JwtSecurityTokenHandler().CanReadToken(response.accessToken));
        _mockAccountRepo.Verify(r => r.GetByUsernameAsync(request.Username), Times.Once);
    }
}
