using CinemaAppContracts.DTO.ReservationDTOs;
using CinemaAppContracts.DTO.SeatDTOs;
using CinemaAppDomain.Entities;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface IReservationService
    {
        Task<ICollection<ReservationForReturningDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ReservationForReturningDTO> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid guid, ReservationForUpdatingDTO tDTO, CancellationToken cancellationToken = default);
        Task<ReservationForReturningDTO> CreateAsync(Guid buyerId, ICollection<SeatForCreatingDTO> seatForCreatingDTOs, CancellationToken cancellationToken = default);
        Task<ReservationForReturningDTO> CreateAsync(string email, ICollection<SeatForCreatingDTO> seatForCreatingDTOs, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<ICollection<ReservationForReturningDTO>> GetMyReservationsAsync(Guid userId, CancellationToken cancellationToken = default);
    }

    public partial class ReservationData
    {
        public ReservationData()
        {

        }
        public ReservationData(ICollection<SeatForCreatingDTO> seatForCreatingDTOs, string email)
        {
            SeatForCreatingDTOs = seatForCreatingDTOs;
            Email = email;
        }

        public ICollection<SeatForCreatingDTO> SeatForCreatingDTOs { get; set; }
        public string Email { get; set; }
    }
}
