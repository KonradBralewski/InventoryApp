using InventoryAppAPI.Models.Authentication;
using InventoryAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.BLL.Services.Authentication;
using InventoryAppAPI.BLL.Services.Email;
using InventoryAppAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace InventoryAppAPI.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<AuthenticationResponse> Login(LoginRegisterRequest dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {

                if (user.EmailConfirmed == false)
                {
                    throw new RequestException(StatusCodes.Status403Forbidden, "Email is not confirmed.");
                }

                var token = await CreateToken(user);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return new AuthenticationResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    ExpirationTime = token.ValidTo
                };
            }

            throw new RequestException(StatusCodes.Status422UnprocessableEntity, "Invalid username or password.");
        }

        public async Task<AuthenticationResponse> Register(LoginRegisterRequest dto)
        {
            var userExist = await CheckIfUserExist(dto.Email!);

            if (userExist == true)
            {
                throw new RequestException(StatusCodes.Status409Conflict, "User already exists!");
            }

            Models.Validators.PasswordValidator.Validate(dto.Password);

            ApplicationUser user = new()
            {
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError, "User creation failed due to an error. Please try again later.");
            }

            var isEmailSend = _emailService.SendEmailConfirmation(dto.Email!);

            if (isEmailSend == false)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "User created but activation link email was not sent due to an error. Please try to resend email again.");
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);

            var token = await CreateToken(user);
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            return new AuthenticationResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpirationTime = token.ValidTo
            };
        }

        public async Task<dynamic> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                throw new RequestException(StatusCodes.Status401Unauthorized, "Invalid client request.");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new RequestException(StatusCodes.Status401Unauthorized, "Invalid access token or refresh token.");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string email = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new RequestException(StatusCodes.Status401Unauthorized, "Invalid access token or refresh token.");
            }

            var newAccessToken = await CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);


            return new AuthenticationResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                ExpirationTime = newAccessToken.ValidTo
            };
        }
        public async Task Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "User not found.");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }
        public async Task RevokeAll()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users.Count() == 0)
            {
                return;
            }

            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
        }


        private async Task<bool> CheckIfUserExist(string email)
        {
            var emailExists = await _userManager.FindByEmailAsync(email);

            if (emailExists != null)
            {
                return true;
            }

            return false;
        }

        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
