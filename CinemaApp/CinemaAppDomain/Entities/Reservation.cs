using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Entities
{
    public class Reservation : BaseEntity
    {
        public Reservation()
        {

        }
        public DateTime TimeOfBuying { get; set; }
        public Guid UserId { get; set; }
        public float Price { get; set; }
        public int Score { get; set; } = 0;
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
