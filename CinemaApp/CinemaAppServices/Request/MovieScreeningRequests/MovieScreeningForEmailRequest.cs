using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.MovieScreeningRequests
{
    public class MovieScreeningForEmailRequest
    {
        public MovieScreeningForEmailRequest()
        {

        }
        public MovieScreeningForEmailRequest(MovieScreening movieScreening, string movieName)
        {
            Id = movieScreening.Id;
            StartTime = movieScreening.StartTime;
            TicketPrice = movieScreening.TicketPrice;
            NumOfColumns = movieScreening.NumOfColumns;
            NumOfRows = movieScreening.NumOfRows;
            MovieName = movieName;
            Status = DateTime.Now < StartTime ? MovieScreeningStatus.AVAILABLE : MovieScreeningStatus.UNAVAILABLE;
        }

        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public float TicketPrice { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfColumns { get; set; }
        public MovieScreeningStatus Status { get; set; }
        public string MovieName { get; set; }
    }
}
