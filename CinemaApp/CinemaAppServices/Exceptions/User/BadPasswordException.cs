using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class BadPasswordException : BadRequestException
    {
        public BadPasswordException() : base("Invalid password")
        {
        }
    }
}
