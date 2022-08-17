using CinemaAppServices.Entities;

namespace CinemaAppContracts.Request.SeatRequests
{
    public class SeatResponse
    {
        public SeatResponse()
        {

        }
        public SeatResponse(Seat seat)
        {
            Id = seat.Id;
            Row = seat.Row;
            Column = seat.Column;
            MovieScreeningId = seat.MovieScreeningId;
        }
        public SeatResponse(Guid id, int row, int column, Guid movieScreeningId)
        {
            Id = id;
            Row = row;
            Column = column;
            MovieScreeningId = movieScreeningId;
        }

        public Guid Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Guid MovieScreeningId { get; set; }
    }
}
