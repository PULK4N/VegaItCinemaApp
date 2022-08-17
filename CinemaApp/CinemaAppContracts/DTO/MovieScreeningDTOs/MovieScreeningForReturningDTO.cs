using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Http;

namespace CinemaAppContracts.DTO.MovieScreeningDTOs
{
    public class MovieScreeningForReturningDTO
    {
        public MovieScreeningForReturningDTO()
        {

        }
        public MovieScreeningForReturningDTO(MovieScreening movieScreening)
        {
            Id = movieScreening.Id;
            StartTime = movieScreening.StartTime;
            TicketPrice = movieScreening.TicketPrice;
            NumOfColumns = movieScreening.NumOfColumns;
            NumOfRows = movieScreening.NumOfRows;
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
