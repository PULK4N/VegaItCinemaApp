using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class PasswordResetFailException : BadRequestException
    {
        public PasswordResetFailException() : base("Password reset fail, token invalid or expired")
        {
        }
    }
}
