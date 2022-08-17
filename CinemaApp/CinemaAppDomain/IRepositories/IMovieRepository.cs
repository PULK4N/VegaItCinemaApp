using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.IRepositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<(ICollection<Movie>, int)> GetAllForCustomer(DateTime date, ICollection<Genre>? genres, bool sortAlphabetically = false,
            bool sortChronologically = false, int pageNumber = 0, int offSet = 0);

        Task<(ICollection<Movie>, int)> GetAllFilteredAsync(
            ICollection<Char> firstLetters,
            string name,
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default
            );

        Task<float> GetMovieScore(Guid movieId);
    }
}
