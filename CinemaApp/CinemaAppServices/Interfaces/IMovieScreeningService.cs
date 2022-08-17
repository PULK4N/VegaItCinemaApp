using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppServices.Entities;
using CinemaAppServices.Filters;
using CinemaAppServices.Request.MovieScreeningRequests;

namespace CinemaAppContracts.Interfaces
{
    public interface IMovieScreeningService : IEntityService<MovieScreening, MovieScreeningCreateRequest, MovieScreeningUpdateRequest, MovieScreeningResponse, ServiceMovieScreeningFilter>
    {
        Task<(ICollection<MovieScreeningForAdminResponse>, int)> GetAllAsyncForAdmin(ServiceMovieScreeningFilter movieScreeningFilter, CancellationToken cancellationToken = default);
    }
}
