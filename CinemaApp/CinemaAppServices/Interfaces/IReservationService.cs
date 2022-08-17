using CinemaAppContracts.Request.ReservationRequests;
using CinemaAppContracts.Request.SeatRequests;
using CinemaAppServices.Entities;

namespace CinemaAppContracts.Interfaces
{
    public interface IReservationService
    {
        Task<ICollection<ReservationResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ReservationResponse> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid guid, ReservationUpdateRequest tDTO, CancellationToken cancellationToken = default);
        Task<ReservationResponse> CreateAsync(Guid buyerId, ICollection<SeatCreateRequest> seatForCreatingDTOs, CancellationToken cancellationToken = default);
        Task<ReservationResponse> CreateAsync(string email, ICollection<SeatCreateRequest> seatForCreatingDTOs, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<ICollection<ReservationResponse>> GetMyReservationsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task RateReservation(Guid userId, Guid id, int score);
    }
}
