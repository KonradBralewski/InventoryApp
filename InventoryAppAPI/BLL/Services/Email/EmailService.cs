using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace InventoryAppAPI.BLL.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmailConfirmation(string receiverEmail)
        {
            var smtp = new SmtpClient();
            bool result;
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("support@trackyourjobs.com"));
                email.To.Add(MailboxAddress.Parse(receiverEmail));
                email.Subject = "Confirm email";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = ""
                };

                // send email
                smtp.Connect(_configuration["SMTP:Host"], int.Parse(_configuration["SMTP:Port"]), SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration["SMTP:Email"], _configuration["SMTP:Password"]);
                smtp.Send(email);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                smtp.Disconnect(true);
            }

            return result;
        }
    }

}
