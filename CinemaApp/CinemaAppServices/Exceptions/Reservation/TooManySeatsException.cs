using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class TooManySeatsException : BadRequestException
    {
        public TooManySeatsException(string message = "Number of seats is too large") : base(message)
        {
        }
    }
}
