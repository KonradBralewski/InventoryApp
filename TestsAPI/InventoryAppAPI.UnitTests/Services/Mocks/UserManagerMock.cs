using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace InventoryAppAPI.UnitTests.Services.Mocks
{
    public static class UserManagerMock
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> list) where TUser : class
        {
            var userStore = new Mock<IUserStore<TUser>>();
            var manager = new Mock<UserManager<TUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            manager.Object.UserValidators.Add(new UserValidator<TUser>());
            manager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            manager.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            manager.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<TUser, string>((x, y) => list.Add(x));
            manager.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            manager.Setup(x => x.AddToRoleAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            return manager;
        }
    }
}
