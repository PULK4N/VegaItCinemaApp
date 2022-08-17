using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.UserRequests
{
    public class UserLoginRequest
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public UserLoginRequest(string emailOrUsername, string password)
        {
            if (string.IsNullOrEmpty(emailOrUsername) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("emailOrUsername", "the email/username and password must not be empty");
            }

            EmailOrUsername = emailOrUsername;
            Password = password;
        }
    }
}
