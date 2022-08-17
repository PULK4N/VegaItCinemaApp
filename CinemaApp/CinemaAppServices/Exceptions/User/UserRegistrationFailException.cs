using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class UserRegistrationFailException : BadRequestException
    {
        public UserRegistrationFailException(string? message) : base($"User registration failed.\n errors:\n{message}")
        {
        }
    }
}
