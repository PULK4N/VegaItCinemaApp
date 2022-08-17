

using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaAppPersistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly DbContext dbContext;
        private readonly DbSet<User> dbSet;

        public UserRepository(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = context.Set<User>();
        }

        public async Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, string includeProperties = "", int first = 0, int offset = 0, CancellationToken cancellationToken = default)
        {
            return dbSet.ToList();
        }

        //TODO: check if this works
        public async Task<User> GetbyIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync(userId);
        }

        public void Insert(User t)
        {
            dbSet.Add(t);
        }

        public void Remove(User t)
        {
            dbSet.Remove(t);
        }

        public void Update(User t)
        {
            dbSet.Update(t);
        }

        public async Task<(ICollection<User>, int)> GetAllFilteredAsync(
                ICollection<Char> firstLetters,
                string name,
                int pageNumber = 1, int pageSize = 10,
                CancellationToken cancellationToken = default
                )
        {
            var firstLetterStrings = firstLetters.Select(c => c.ToString()).ToList();

            int numOfLetters = firstLetters.Count();

            IQueryable<User> query = dbSet;

            switch (numOfLetters, name)
            {
                case (0, ""):
                    break;
                case var tuple when tuple.numOfLetters == 0 && tuple.name != "":
                    //TODO: na ostalim promeniti, kao i api
                    query = query.Where(user => user.UserName.Contains(name));
                    break;
                case var tuple when tuple.numOfLetters != 0 && tuple.name == "":
                    query = query.Where(user => firstLetterStrings.Contains(user.UserName.Substring(0, 1)));
                    break;
                default:
                    query = query.Where(user => firstLetterStrings.Contains(user.UserName.Substring(0, 1)) || user.UserName.Contains(name));
                    break;
            }




            if (pageNumber < 0 || pageSize < 0)
                throw new Exception("Page size or page number less than zero");
            int numOfUsers = query.Count();
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);


            return (await query.ToListAsync(cancellationToken), numOfUsers);
        }

    }
}
