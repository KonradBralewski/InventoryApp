using InventoryAppAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryAppAPI.Controllers.InventoryControllers.Abstract
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")]
    public abstract class InventoryAppController : ControllerBase
    {
        protected int GetCallerId()
        {
            Claim userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                return Int32.Parse(userId.Value);
            }

            throw new RequestException(StatusCodes.Status401Unauthorized, "Couldn't receive information about request sending user.");
        }
    }
}
