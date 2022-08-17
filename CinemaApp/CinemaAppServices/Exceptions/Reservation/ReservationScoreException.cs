using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class ReservationScoreException : BadRequestException
    {
        public ReservationScoreException() : base("Invalid value for score, score should be from 1 to 5")
        {
        }
    }
}
