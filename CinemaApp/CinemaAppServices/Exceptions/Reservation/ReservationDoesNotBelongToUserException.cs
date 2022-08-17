using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class ReservationDoesNotBelongToUserException : BadRequestException
    {
        public ReservationDoesNotBelongToUserException() : base("Reservation does not belong to the user")
        {
        }
    }
}
