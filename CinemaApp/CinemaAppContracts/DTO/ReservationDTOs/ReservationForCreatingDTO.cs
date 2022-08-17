using CinemaAppContracts.DTO.SeatDTOs;
using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.ReservationDTOs
{
    public class ReservationForCreatingDTO
    {
        public ReservationForCreatingDTO()
        {
        }
        public ReservationForCreatingDTO(ICollection<SeatForCreatingDTO> seatForCreatingDTOs, string email)
        {
            Seats = seatForCreatingDTOs;
            Email = email;
        }
        public ICollection<SeatForCreatingDTO> Seats { get; set; } = new List<SeatForCreatingDTO>();
        public string Email { get; set; }
    }
}
