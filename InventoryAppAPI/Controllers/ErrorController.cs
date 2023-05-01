using InventoryAppAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;

            if (exception.GetType() == typeof(RequestException)) 
            {
                RequestException reqException = (RequestException)exception;
                return StatusCode(reqException.StatusCode, reqException.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Server could not perform your request.");
        }
    }
}
