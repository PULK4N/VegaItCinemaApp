using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.MovieScreening
{
    public class MovieScreeningContainsReservationException : BadRequestException
    {
        public MovieScreeningContainsReservationException(Guid id) : base($"Movie screening with an Id {id} has reservation and can't be deleted")
        {

        }
    }
}
