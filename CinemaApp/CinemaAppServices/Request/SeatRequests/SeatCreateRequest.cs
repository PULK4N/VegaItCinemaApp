using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.SeatRequests
{
    public class SeatCreateRequest
    {
        public SeatCreateRequest()
        {

        }
        public SeatCreateRequest(Seat seat)
        {
            Row = seat.Row;
            Column = seat.Column;
            MovieScreeningId = seat.MovieScreeningId;
        }
        public SeatCreateRequest(int row, int column, Guid movieScreeningId)
        {
            Row = row;
            Column = column;
            MovieScreeningId = movieScreeningId;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public Guid MovieScreeningId { get; set; }
    }
}
