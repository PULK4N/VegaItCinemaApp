using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppPersistence.Repositories
{
    internal sealed class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {

        }

        public async Task<Genre> GetByName(string name)
        {
            return await dbSet.FirstOrDefaultAsync(genre => genre.Name == name);
        }

        public async Task<ICollection<Genre>> GetAllByIds(ICollection<Guid> Ids, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(g => Ids.Contains(g.Id)).ToListAsync();
        }

        public async Task<(ICollection<Genre>, int)> GetAllFilteredAsync(
        ICollection<Char> firstLetters,
        string name,
        int pageNumber = 1, int pageSize = 10,
        CancellationToken cancellationToken = default
        )
        {
            var firstLetterStrings = firstLetters.Select(c => c.ToString()).ToList();

            int numOfLetters = firstLetters.Count();

            IQueryable<Genre> query = dbSet;

            switch (numOfLetters,name)
            {
                case (0, ""):
                    break;
                case var tuple when tuple.numOfLetters == 0 && tuple.name != "":
                    //TODO: na ostalim promeniti, kao i api
                    query = query.Where(genre => genre.Name.Contains(name));
                    break;
                case var tuple when tuple.numOfLetters != 0 && tuple.name == "":
                    query = query.Where(genre => firstLetterStrings.Contains(genre.Name.Substring(0, 1)));
                    break;
                default:
                    query = query.Where(genre => firstLetterStrings.Contains(genre.Name.Substring(0, 1)) || genre.Name.Contains(name));
                    break;
            }

            if (pageNumber < 0 || pageSize < 0)
                throw new Exception("Page size or page number less than zero");

            int numOfGenres = query.Count();
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);


            return (await query.ToListAsync(cancellationToken), numOfGenres);
        }

    }
}
