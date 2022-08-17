using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class BadSeatRowNumberException : BadRequestException
    {
        public BadSeatRowNumberException(int rowNum, int movieScreeningRows) : 
            base($"Seat row can't be {rowNum} when number of rows for movie screening is {movieScreeningRows}")
        {
        }
    }
}
