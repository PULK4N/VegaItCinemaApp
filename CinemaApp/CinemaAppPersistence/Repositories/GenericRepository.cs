using System.Linq.Expressions;
using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CinemaAppPersistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual async Task<ICollection<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            int first = 0, int offset = 0,
            CancellationToken cancellationToken = default
            )
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (offset > 0)
            {
                query = query.Skip(offset);
            }
            if (first > 0)
            {
                query = query.Take(first);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken);
            }
        }

        public virtual async Task<TEntity> GetbyIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public virtual void Insert(TEntity t)
        {
            dbSet.Add(t);
        }

        public virtual void Remove(TEntity t)
        {
            dbSet.Remove(t);
        }

        public virtual void Update(TEntity t)
        {
            dbSet.Update(t);
        }

        public async virtual Task<int> GetCount()
        {
            return await dbSet.CountAsync();
        }
    }
}
