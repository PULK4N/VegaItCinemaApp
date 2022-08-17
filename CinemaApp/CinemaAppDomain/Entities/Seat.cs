using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Entities
{
    public class Seat : BaseEntity
    {
        public int Row { get; set; }
        public int Column { get; set; }

        //[Required(ErrorMessage = "Movie screening is required")]
        public Guid MovieScreeningId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
