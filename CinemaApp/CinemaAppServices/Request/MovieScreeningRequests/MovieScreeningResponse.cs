using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Http;

namespace CinemaAppContracts.Request.MovieScreeningRequests
{
    public class MovieScreeningResponse
    {
        public MovieScreeningResponse()
        {
            Status = DateTime.Now < StartTime ? MovieScreeningStatus.AVAILABLE : MovieScreeningStatus.UNAVAILABLE;
        }

        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public float TicketPrice { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfColumns { get; set; }
        public MovieScreeningStatus Status { get; set; }
    }
}
