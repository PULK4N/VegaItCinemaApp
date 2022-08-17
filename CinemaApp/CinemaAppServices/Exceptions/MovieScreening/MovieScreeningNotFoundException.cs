using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.MovieScreening
{
    public class MovieScreeningNotFoundException : NotFoundException
    {
        public MovieScreeningNotFoundException(Guid movieScreeningId) : base($"Movie screening with Id \"{movieScreeningId}\" not found")
        {
        }
    }
}
