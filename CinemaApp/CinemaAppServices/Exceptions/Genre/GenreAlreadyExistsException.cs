using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Genre
{
    public class GenreAlreadyExistsException : BadRequestException
    {
        public GenreAlreadyExistsException(string name) : base($"The genre with the name {name} already exists.")
        {

        }
    }
}
