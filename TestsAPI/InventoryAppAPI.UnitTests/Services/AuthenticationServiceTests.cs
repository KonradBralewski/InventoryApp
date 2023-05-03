using InventoryAppAPI.BLL.Services;
using InventoryAppAPI.BLL.Services.Email;
using InventoryAppAPI.BLL.Token;
using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Authentication;
using InventoryAppAPI.UnitTests.Services.Mocks;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryAppAPI.UnitTests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _authService;
        private List<ApplicationUser> _users = new List<ApplicationUser>();

        // MOCKS
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<ITokenManager> _tokenManagerMock;

        public AuthenticationServiceTests()
        {
            _userManagerMock = UserManagerMock.MockUserManager(_users);
            _tokenManagerMock = TokenManagerMock.MockTokenManager();

            _authService = new AuthenticationService(_userManagerMock.Object, _tokenManagerMock.Object);
        }

        // REGISTER
        // REGISTER
        // REGISTER

        [Fact]
        public async Task Register_UniqueCredentialsRequestGiven_ShouldRegisterUser_ShouldReturnAuthenticationResponse()
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Email = "john.doe@example.com",
                Password = "Abcd1234@"
            };

            // Act
            var result = await _authService.Register(request);

            // Assert
            Assert.True(_users.Count() > 0);
            Assert.NotNull(result);
            Assert.Equal(request.Email, result.Email);
            Assert.NotNull(result.Token);
            Assert.NotNull(result.RefreshToken);
            Assert.NotEqual(DateTime.MinValue, result.ExpirationTime);
        }

        [Fact]
        public async Task Register_ExistingUserRequestGiven_ShouldThrowRequestException()
        {
            // Arrange
            var existingUser = new ApplicationUser { Email = "john.doe@example.com" };
            _users.Add(existingUser);

            var request = new LoginRegisterRequest
            {
                Email = existingUser.Email,
                Password = "Abcd1234"
            };

            // Act & Assert
            await Assert.ThrowsAsync<RequestException>(() => _authService.Register(request));
        }

        [Theory]
        [InlineData("john.doeexample.com")]
        [InlineData("john.doe@")]
        [InlineData("john.doe@.")]
        [InlineData("john.doe@.com")]
        public async Task Register_InvalidEmailRequestGiven_ShouldThrowRequestException(string email)
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Email = email,
                Password = "Abcd1234@"
            };

            // Act & Assert
            await Assert.ThrowsAsync<RequestException>(async () => await _authService.Register(request));
        }

        [Theory]
        [InlineData("Abcd1234")]
        [InlineData("abcd1234")]
        [InlineData("1234abcd")]
        public async Task Register_InvalidPasswordRequestGiven_ShouldThrowRequestException(string password)
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Email = "john.doe@example.com",
                Password = password
            };

            // Act & Assert
            await Assert.ThrowsAsync<RequestException>(async () => await _authService.Register(request));
        }


        // LOGIN
        // LOGIN
        // LOGIN

        [Fact]
        public async Task Login_ExistingCredentialsGiven_ReturnsAuthenticationResponse()
        {
            // Arrange
            LoginRegisterRequest request = new LoginRegisterRequest{
                Email = "johndoe@doe.com",
                Password = "Testpassw@rd1"
            };

            await _authService.Register(request);

            _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _authService.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Email, result.Email);
            Assert.NotEmpty(result.Token);
            Assert.NotEmpty(result.RefreshToken);
            Assert.NotEqual(DateTime.MinValue, result.ExpirationTime);
        }

        [Fact]
        public async Task Login_InvalidCredentialsRequestGiven_ThrowsRequestException()
        {
            // Arrange
            LoginRegisterRequest request = new LoginRegisterRequest
            {
                Email = "johndoe@doe.com",
                Password = "Testpassw@rd1"
            };

            await _authService.Register(request);

            _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            request.Email = request.Email.Replace(".", ",");

            // Act & Assert
            await Assert.ThrowsAsync<RequestException>(() => _authService.Login(request));
        }
    }
}
