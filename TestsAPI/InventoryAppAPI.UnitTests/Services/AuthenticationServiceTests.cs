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
        private readonly UserManager<ApplicationUser> _userManagerMock;
        private readonly IEmailService _emailServiceMock;
        private readonly ITokenManager _tokenManagerMock;

        public AuthenticationServiceTests()
        {
            _userManagerMock = UserManagerMock.MockUserManager<ApplicationUser>(_users).Object;
            _emailServiceMock = EmailServiceMock.MockEmailService().Object;
            _tokenManagerMock = TokenManagerMock.MockTokenManager().Object;

            _authService = new AuthenticationService(_userManagerMock, _emailServiceMock, _tokenManagerMock);
        }

        [Fact]
        public async Task Register_ValidRequestGiven_ShouldRegisterUser_ShouldReturnAuthenticationResponse()
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Email = "john.doe@example.com",
                Password = "Abcd1234@"
            };

            // Act
            var result = await _authService.Register(request);
            zz
            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Email, result.Email);
            Assert.NotNull(result.Token);
            Assert.NotNull(result.RefreshToken);
            Assert.NotEqual(default, result.ExpirationTime);
        }

        [Fact]
        public async Task Register_GivenExistingUserRequest_ShouldThrowRequestException()
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
        public async Task Register_GivenInvalidEmailRequest_ShouldThrowRequestException(string email)
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Email = email,
                Password = "Abcd1234"
            };

            // Act & Assert
            await Assert.ThrowsAsync<RequestException>(() => _authService.Register(request));
        }

        [Theory]
        [InlineData("Abcd1234")]
        [InlineData("abcd1234")]
        [InlineData("Abcd123!")]
        [InlineData("1234abcd")]
        public async Task Register_GivenInvalidPasswordRequest_ShouldThrowRequestException(string password)
        {
            // Arrange
            var request = new LoginRegisterRequest
            {
                Email = "john.doe@example.com",
                Password = password
            };

            // Act & Assert
            await Assert.ThrowsAsync<RequestException>(() => _authService.Register(request));
        }
    }
}
