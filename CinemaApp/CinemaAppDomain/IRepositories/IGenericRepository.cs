using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.IRepositories
{
    public interface IGenericRepository<T>
    {
        Task<ICollection<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int first = 0, int offset = 0,
            CancellationToken cancellationToken = default
            );
        Task<T> GetbyIdAsync(Guid genreId, CancellationToken cancellationToken = default);
        void Update(T t);
        void Insert(T t);
        void Remove(T t);
    }
}
