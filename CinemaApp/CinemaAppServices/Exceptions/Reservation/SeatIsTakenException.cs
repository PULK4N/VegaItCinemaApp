using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class SeatIsTakenException : BadRequestException
    {
        public SeatIsTakenException() : base("Seat has already been taken")
        {
        }
    }
}
