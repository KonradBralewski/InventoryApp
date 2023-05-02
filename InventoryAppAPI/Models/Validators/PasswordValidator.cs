using InventoryAppAPI.Exceptions;
using System.Text.RegularExpressions;

namespace InventoryAppAPI.Models.Validators
{
    public class PasswordValidator
    {
        public static void Validate(string password, string oldPassword = null)
        {
            var regexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";

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

            if (!Regex.Match(password, regexPattern).Success)
            {
                throw new RequestException(StatusCodes.Status422UnprocessableEntity,
                    "Password must contain at least one uppercase letter, one special character (!@#$%^&*) and one digit.");

            }
        }

    }
}
