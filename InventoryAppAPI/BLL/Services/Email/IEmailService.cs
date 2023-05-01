namespace InventoryAppAPI.BLL.Services.Email
{
    public interface IEmailService
    {
        bool SendEmailConfirmation(string receiverEmail);
    }
}
