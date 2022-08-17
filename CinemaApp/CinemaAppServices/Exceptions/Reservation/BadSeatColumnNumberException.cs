using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.Reservation
{
    public class BadSeatColumnNumberException : BadRequestException
    {
        public BadSeatColumnNumberException(int colNum, int movieScreeningRows) :
            base($"Seat column can't be {colNum} when number of columns for movie screening is {movieScreeningRows}")
        {

        }
    }
}
