using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class ReservationNotFoundException : NotFoundException
    {
        public ReservationNotFoundException(Guid reservationId) : base($"Reservation with Id: \"{reservationId}\" not found")
        {
        }
    }
}
