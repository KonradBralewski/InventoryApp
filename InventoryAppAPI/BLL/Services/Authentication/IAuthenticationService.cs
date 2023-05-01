using InventoryAppAPI.Models;
using InventoryAppAPI.Models.Authentication;

namespace InventoryAppAPI.BLL.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginRegisterRequest dto);
        Task<AuthenticationResponse> Register(LoginRegisterRequest dto);
    }
}
