using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Exceptions.MovieScreening
{
    public class TicketPriceLessThanZeroException : BadRequestException
    {
        public TicketPriceLessThanZeroException() : base("Ticket price can't be less than zero")
        {

        }
    }
}
