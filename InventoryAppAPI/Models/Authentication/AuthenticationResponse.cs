namespace InventoryAppAPI.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        
        public string RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
