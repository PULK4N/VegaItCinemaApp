using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.User
{
    public class UserEmailIsNotConfirmedException : BadRequestException
    {
        public UserEmailIsNotConfirmedException() : base("User can't login since email is not confirmed")
        {
        }
    }
}
