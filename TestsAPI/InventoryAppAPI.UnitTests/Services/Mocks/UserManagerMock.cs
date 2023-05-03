using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace InventoryAppAPI.UnitTests.Services.Mocks
{
    public static class UserManagerMock
    {
        public static Mock<UserManager<ApplicationUser>> MockUserManager(List<ApplicationUser> list)
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var manager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            manager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            manager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            manager.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            manager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<ApplicationUser, string>((x, y) => { x.EmailConfirmed = true; list.Add(x); });
            manager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            manager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            manager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(async (string email) => 
            list.FirstOrDefault(u => u.Email == email));

            return manager;
        }

    }
}
