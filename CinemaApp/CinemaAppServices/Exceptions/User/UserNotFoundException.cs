using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId) : base($"User with Id \"{userId}\" has not been found")
        {

        }
        public UserNotFoundException(string emailOrUsername) : base($"User with email or username: \"{emailOrUsername}\" has not been found")
        {

        }
    }
}
