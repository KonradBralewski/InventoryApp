using InventoryAppAPI.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRegisterRequest request)
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(LoginRegisterRequest request)
        {
            return Ok();
        }
    }
}
