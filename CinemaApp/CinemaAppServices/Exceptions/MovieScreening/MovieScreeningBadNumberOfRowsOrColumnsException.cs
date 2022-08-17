using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.MovieScreening
{
    public class MovieScreeningBadNumberOfRowsOrColumnsException : BadRequestException
    {
        public MovieScreeningBadNumberOfRowsOrColumnsException() : base("Number of rows or columns cannot be less than zero")
        {

        }
    }
}
