using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Entities
{
    public class MovieScreening : BaseEntity
    {
        public Guid MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public float TicketPrice { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfColumns { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
