using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Request.MovieScreeningRequests
{
    public class MovieScreeningForAdminResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public float TicketPrice { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfColumns { get; set; }
        public MovieScreeningStatus Status { get; set; }
    }
}
