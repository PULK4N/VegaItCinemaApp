using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.MovieScreeningRequests
{
    public class MovieScreeningUpdateRequest
    {
        public MovieScreeningUpdateRequest()
        {

        }
        public MovieScreeningUpdateRequest(Guid id, DateTime startTime, float ticketPrice, int numOfRows, int numOfColumns)
        {
            Id = id;
            StartTime = startTime;
            TicketPrice = ticketPrice;
            NumOfRows = numOfRows;
            NumOfColumns = numOfColumns;
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
