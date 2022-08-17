using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.MovieScreening
{
    public class MovieScreeningBadTimeSchedulingException : BadRequestException
    {
        public MovieScreeningBadTimeSchedulingException() : base($"Movie Screening can't be scheduled to the past")
        {

        }
    }
}
