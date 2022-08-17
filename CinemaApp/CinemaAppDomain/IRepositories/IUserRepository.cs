using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<(ICollection<User>, int)> GetAllFilteredAsync(
            ICollection<Char> firstLetters,
            string name,
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default
            );
    }
}
