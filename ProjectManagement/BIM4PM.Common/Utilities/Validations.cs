using System.Net.Mail;

namespace BIM4PM.Common
{
    public static class Validations
    {
        /// <summary>
        /// This is a basic email address validation check.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if email address is valid.</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}