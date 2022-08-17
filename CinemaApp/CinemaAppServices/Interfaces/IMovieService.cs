using CinemaAppContracts.Request.MovieRequests;
using CinemaAppServices.Entities;
using CinemaAppServices.Filters;

namespace CinemaAppContracts.Interfaces
{
    public interface IMovieService : IEntityService<Movie, MovieCreateRequest, MovieUpdateRequest, MovieResponse, ServiceMovieFilter>
    {
        public Task<(ICollection<MovieResponse>,int) > GetAllAsync(ServiceMovieCustomerFilter movieCustomerFilter, CancellationToken cancellationToken = default);
        public Task UpdateScore(Guid movieId);
    }
}
