using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.MovieScreeningDTOs
{
    public class MovieScreeningForCreatingDTO
    {
        public MovieScreeningForCreatingDTO()
        {

        }
        public MovieScreeningForCreatingDTO(MovieScreening movieScreening)
        {
            Id = movieScreening.Id;
            MovieId = movieScreening.MovieId;
            StartTime = movieScreening.StartTime;
            TicketPrice = movieScreening.TicketPrice;
            NumOfColumns = movieScreening.NumOfColumns;
            NumOfRows = movieScreening.NumOfRows;
            Status = DateTime.Now < StartTime ? MovieScreeningStatus.AVAILABLE : MovieScreeningStatus.UNAVAILABLE; ;
        }
        public MovieScreeningForCreatingDTO(Guid id, Guid movieId, DateTime startTime, float ticketPrice, int numOfRows, int numOfColumns)
        {
            Id = id;
            MovieId = movieId;
            StartTime = startTime;
            TicketPrice = ticketPrice;
            NumOfRows = numOfRows;
            NumOfColumns = numOfColumns;
            Status = DateTime.Now < StartTime ? MovieScreeningStatus.AVAILABLE : MovieScreeningStatus.UNAVAILABLE;
        }

        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public float TicketPrice { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfColumns { get; set; }
        public MovieScreeningStatus Status { get; set; }
    }
}
