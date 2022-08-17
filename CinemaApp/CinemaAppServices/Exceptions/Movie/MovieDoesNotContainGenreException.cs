using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Movie
{
    public class MovieDoesNotContainGenreException : BadRequestException
    {
        public MovieDoesNotContainGenreException(Guid movieId, Guid GenreId) : base($"Movie with an Id: {movieId} does not contain genre with an Id: {GenreId}")
        {

        }
    }
}
