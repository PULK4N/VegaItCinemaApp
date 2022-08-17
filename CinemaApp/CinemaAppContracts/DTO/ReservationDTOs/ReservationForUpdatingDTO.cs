using CinemaAppContracts.DTO.SeatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.ReservationDTOs
{
    public class ReservationForUpdatingDTO
    {
        public ReservationForUpdatingDTO()
        {
        }
        public ReservationForUpdatingDTO(Guid id, DateTime timeOfBuying, float price)
        {
            Id = id;
            TimeOfBuying = timeOfBuying;
            Price = price;
        }
        public Guid Id { get; set; }
        public DateTime TimeOfBuying { get; set; }
        public float Price { get; set; }
        public ICollection<SeatDTO> Seats { get; set; } = new List<SeatDTO>();
    }
}
