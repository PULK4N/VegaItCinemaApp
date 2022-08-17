using CinemaAppContracts.Request.SeatRequests;
using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.ReservationRequests
{
    public class ReservationResponse
    {
        public ReservationResponse()
        {

        }
        public ReservationResponse(Guid id, DateTime timeOfBuying, float price)
        {
            Id = id;
            TimeOfBuying = timeOfBuying;
            Price = price;
        }
        public Guid Id { get; set; }
        public DateTime TimeOfBuying { get; set; }
        public float Price { get; set; }
        public ICollection<SeatResponse> Seats { get; set; } = new List<SeatResponse>();
    }
}
