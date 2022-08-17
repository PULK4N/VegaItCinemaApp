using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Movie
{
    public class MovieNotFoundException : NotFoundException
    {
        public MovieNotFoundException(Guid guid) : base($"Movie with an Id \"{guid}\" is not found")
        {

        }
    }
}
