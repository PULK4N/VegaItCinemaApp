using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.MovieScreening
{
    public class MovieScreeningStartedException : BadRequestException
    {
        public MovieScreeningStartedException(string message = "Movie screening has already started") : base(message)
        {
        }
    }
}
