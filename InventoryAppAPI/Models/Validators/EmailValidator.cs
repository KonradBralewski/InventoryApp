using InventoryAppAPI.Exceptions;
using System.Text.RegularExpressions;

namespace InventoryAppAPI.Models.Validators
{
    public class EmailValidator
    {
        public static void Validate(string email)
        {
            var regexPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (email.Length < 3)
            {
                throw new RequestException(StatusCodes.Status422UnprocessableEntity,
                    "Email is too short, please enter valid email address.");

            }

            if (!Regex.IsMatch(email, regexPattern))
            {
                throw new RequestException(StatusCodes.Status422UnprocessableEntity,
                    "Please enter valid email address.");

            }
        }

    }
}
