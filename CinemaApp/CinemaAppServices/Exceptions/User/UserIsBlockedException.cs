using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class UserIsBlockedException : BadRequestException
    {
        public UserIsBlockedException(Guid userId) : base($"User with an Id:{userId} is blocked")
        {
        }
    }
}
