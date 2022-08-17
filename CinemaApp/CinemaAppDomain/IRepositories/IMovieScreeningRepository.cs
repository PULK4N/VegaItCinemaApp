using CinemaAppEntities.Requests;
using CinemaAppServices.Entities;

namespace CinemaAppServices.IRepositories
{
    public interface IMovieScreeningRepository : IGenericRepository<MovieScreening>
    {
        Task<(ICollection<MovieScreeningForAdminResponseModel>, int)> GetAllFilteredAsync(
            ICollection<Char> firstLetters,
            string name,
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default
            );
    }
}
