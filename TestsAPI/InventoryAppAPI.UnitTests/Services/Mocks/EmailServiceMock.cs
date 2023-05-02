using InventoryAppAPI.BLL.Services.Email;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppAPI.UnitTests.Services.Mocks
{
    public static class EmailServiceMock
    {
        public static Mock<IEmailService> MockEmailService()
        {
            var emailService = new Mock<IEmailService>();
            emailService.Setup(x => x.SendEmailConfirmation(It.IsAny<string>())).Returns(true);

            return emailService;
        }
    }
}
