using InventoryAppAPI.Exceptions;
using System.Text.RegularExpressions;

namespace InventoryAppAPI.Models.Validators
{
    public class PasswordValidator
    {
        public static void Validate(string password, string oldPassword = null)
        {
            var regexPassword = new Regex("^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*\\d)[A-Za-z\\d!@#$%^&*]{8,}$");

            if (oldPassword != null)
            {
                if (password == oldPassword)
                {
                    throw new RequestException(StatusCodes.Status422UnprocessableEntity, "The new password must be different from the old password.");
                }
            }

            if (password.Length < 8)
            {
                throw new RequestException(StatusCodes.Status422UnprocessableEntity, "Password is too short, the minimum number of characters is 8.");

            }

            if (!regexPassword.IsMatch(password))
            {
                throw new RequestException(StatusCodes.Status422UnprocessableEntity,
                    "Password must contain at least one uppercase letter, one special character (!@#$%^&*) and one digit.");

            }
        }

    }
}
