using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace InventoryAppAPI.BLL.Token
{
    public interface ITokenManager
    {
        public Task<TokenModel> GenerateToken(ApplicationUser user);
        public Task<TokenModel> RefreshToken(TokenModel tokenModel);
        public Task Revoke(string username);
        public Task RevokeAll();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


    }
}
