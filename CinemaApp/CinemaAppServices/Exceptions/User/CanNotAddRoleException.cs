using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class CanNotAddRoleException : BadRequestException
    {
        public CanNotAddRoleException(string roleName) : base($"Can't give user a role {roleName}, check if user already has the role")
        {
        }
    }
}
