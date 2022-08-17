using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class NoSeatsToReserveException : BadRequestException
    {
        public NoSeatsToReserveException() : base("Reservation contains no seats to reserve")
        {

        }
    }
}
