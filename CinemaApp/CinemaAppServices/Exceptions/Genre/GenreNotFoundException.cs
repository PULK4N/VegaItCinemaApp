using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Genre
{
    public class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(Guid genreId) : base($"The genre with the identifier \"{genreId}\" was not found.")
        {
        }
    }
}
