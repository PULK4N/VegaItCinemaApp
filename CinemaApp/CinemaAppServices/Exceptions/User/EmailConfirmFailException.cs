using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class EmailConfirmFailException : BadRequestException
    {
        public EmailConfirmFailException() : base("Failed to confirm email, token sesion might have expired")
        {
        }
    }
}
