using Microsoft.AspNetCore.Identity;

namespace InventoryAppAPI.DAL.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public override string UserName // Treat username as email to avoid 'invalid username' creation error. Only email is needed for authentication.
        {
            get { return base.Email; }
        }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

}
