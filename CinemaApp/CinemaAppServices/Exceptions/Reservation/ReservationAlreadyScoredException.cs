using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class ReservationAlreadyScoredException : BadRequestException
    {
        public ReservationAlreadyScoredException() : base("Reservation has already been scored")
        {
        }
    }
}
