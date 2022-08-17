using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.IRepositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        public Task<Genre> GetByName(string name);

        Task<(ICollection<Genre>, int)> GetAllFilteredAsync(
            ICollection<Char> firstLetters,
            string name,
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default
            );
        Task<ICollection<Genre>> GetAllByIds(ICollection<Guid> Ids, CancellationToken cancellationToken = default);
    }
}
