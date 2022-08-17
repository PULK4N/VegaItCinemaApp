using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Movie
{
    public class MoviesForbidenSearchPeriodException : BadRequestException
    {
        public MoviesForbidenSearchPeriodException(string message = "Can't lookup a movie for a period after 7 days") : base(message)
        {
        }
    }
}
