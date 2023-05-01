using Microsoft.AspNetCore.Identity;

namespace InventoryAppAPI.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

}
