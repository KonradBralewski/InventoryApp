using InventoryAppAPI.BLL.Services.Authentication;
using InventoryAppAPI.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRegisterRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(LoginRegisterRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }
    }
}
