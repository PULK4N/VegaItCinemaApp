using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.SeatDTOs
{
    public class SeatForCreatingDTO
    {
        public SeatForCreatingDTO()
        {

        }
        public SeatForCreatingDTO(Seat seat)
        {
            Row = seat.Row;
            Column = seat.Column;
            MovieScreeningId = seat.MovieScreeningId;
        }
        public SeatForCreatingDTO(int row, int column, Guid movieScreeningId)
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
