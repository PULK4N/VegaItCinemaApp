using CinemaAppContracts.Request.SeatRequests;
using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.ReservationRequests
{
    public class ReservationCreateRequest
    {
        public ReservationCreateRequest()
        {

        }
        public ReservationCreateRequest(ICollection<SeatCreateRequest> seatForCreatingDTOs)
        {
            Seats = seatForCreatingDTOs;
        }
        public ICollection<SeatCreateRequest> Seats { get; set; } = new List<SeatCreateRequest>();
    }
}
