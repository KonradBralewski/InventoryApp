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
using InventoryAppAPI.BLL.Token;

namespace InventoryAppAPI.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ITokenManager _tokenManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IEmailService emailService, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _tokenManager = tokenManager;
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

                var tokenModel = await _tokenManager.GenerateToken(user);

                await UserUpdateRoutine(user, tokenModel);

                return new AuthenticationResponse
                {
                    Email = user.Email,
                    Token = tokenModel.AccessToken,
                    RefreshToken = tokenModel.RefreshToken,
                    ExpirationTime = tokenModel.RefreshTokenExpiryTime
                };
            }

            throw new RequestException(StatusCodes.Status422UnprocessableEntity, "Invalid username or password.");
        }

        public async Task<AuthenticationResponse> Register(LoginRegisterRequest dto)
        {
            var userExist = CheckIfUserExists(dto.Email!);

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

            var tokenModel = await _tokenManager.GenerateToken(user);

            await UserUpdateRoutine(user, tokenModel);

            return new AuthenticationResponse
            {
                Email = user.Email,
                Token = tokenModel.AccessToken,
                RefreshToken = tokenModel.RefreshToken,
                ExpirationTime = tokenModel.RefreshTokenExpiryTime
            };
        }
        private bool CheckIfUserExists(string email)
        {
            return _userManager.Users.Any(u => u.Email == email);
        }

        private async Task UserUpdateRoutine(ApplicationUser user, TokenModel tm) 
        {
            user.RefreshToken = tm.RefreshToken;
            user.RefreshTokenExpiryTime = tm.RefreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);
        }
    }
    

    
}
