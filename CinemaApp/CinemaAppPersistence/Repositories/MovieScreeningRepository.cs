using CinemaAppEntities.Requests;
using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CinemaAppPersistence.Repositories
{
    internal sealed class MovieScreeningRepository : GenericRepository<MovieScreening>, IMovieScreeningRepository
    {
        public MovieScreeningRepository(DbContext context) : base(context)
        {
        }

        public async Task<(ICollection<MovieScreeningForAdminResponseModel>, int)> GetAllFilteredAsync(ICollection<char> firstLetters, string name, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var Movies = dbContext.Set<Movie>();
            var query = from ms in dbSet
                        join m in Movies on ms.MovieId equals m.Id
                        select new { m.Name, ms.Id, ms.NumOfRows, ms.NumOfColumns, ms.StartTime, ms.TicketPrice, ms.MovieId };


            var firstLetterStrings = firstLetters.Select(c => c.ToString()).ToList();

            int numOfLetters = firstLetters.Count();

            switch (numOfLetters, name)
            {
                case (0, ""):
                    break;
                case var tuple when tuple.numOfLetters == 0 && tuple.name != "":
                    //TODO: na ostalim promeniti, kao i api
                    query = query.Where(movie => movie.Name.Contains(name));
                    break;
                case var tuple when tuple.numOfLetters != 0 && tuple.name == "":
                    query = query.Where(movie => firstLetterStrings.Contains(movie.Name.Substring(0, 1)));
                    break;
                default:
                    query = query.Where(movie => firstLetterStrings.Contains(movie.Name.Substring(0, 1)) || movie.Name.Contains(name));
                    break;
            }

            var newQuery = query.Select(a => new MovieScreeningForAdminResponseModel
            {
                Id = a.Id,
                NumOfRows = a.NumOfRows,
                NumOfColumns = a.NumOfColumns,
                StartTime = a.StartTime,
                TicketPrice = a.TicketPrice,
                Name = a.Name
            });

            int numOfMovieScreenings = query.Count();
            newQuery = newQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return (await newQuery.ToListAsync(cancellationToken), numOfMovieScreenings);
        }

        public override async Task<MovieScreening> GetbyIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await dbSet.Include(x => x.Seats).FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
    }
}
