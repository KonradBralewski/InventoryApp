using InventoryAppAPI.BLL.Token;
using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppAPI.UnitTests.Services.Mocks
{
    public static class TokenManagerMock
    {
        public static Mock<ITokenManager> MockTokenManager()
        {
            var tokenManager = new Mock<ITokenManager>();

            tokenManager.Setup((x) => x.GenerateToken(It.IsAny<ApplicationUser>()))
               .Returns(Task.FromResult(new TokenModel
               {
                   AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
               "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ." +
               "SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                   RefreshToken = "5c877e4a-6b4a-4b1a-8f0a-2d8b8b4e5d0a"
               }));
              

            return tokenManager;
        }

    }
}
