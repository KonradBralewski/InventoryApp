using InventoryAppAPI.BLL.Services.Authentication;
using InventoryAppAPI.BLL.Services.Email;
using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
using InventoryAppAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : InventoryAppController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;

        public AuthenticationController(IAuthenticationService authenticationService, IEmailService emailService)
        {
            _authenticationService = authenticationService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRegisterRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(LoginRegisterRequest request)
        {
            var response = await _authenticationService.Register(request);

           // _emailService.SendEmailConfirmation(request.Email);

            return Ok(await _authenticationService.Register(request));
        }
    }
}
