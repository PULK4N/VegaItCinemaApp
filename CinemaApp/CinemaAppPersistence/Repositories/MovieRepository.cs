using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaAppPersistence.Repositories
{
    internal sealed class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }
        public async Task<(ICollection<Movie>,int)> GetAllForCustomer(DateTime date, ICollection<Genre>? genres, bool sortAlphabetically = false,
            bool sortChronologically = false, int pageNumber = 1, int pageSize = 10)
        {
            //bool firstIteration = true;
            var movieScreenings = dbContext.Set<MovieScreening>();
            var movieGenres = dbContext.Set<Genre>();
            var genreIds = genres.Select(g => g.Id).ToList();
            IQueryable<Movie> query = dbSet.Include(m => m.MovieScreenings);
            if(genreIds.Count() != 0)
                query = query.Where(movie => movie.Genres.Any(genre => genreIds.Contains(genre.Id)));
            query = query.Where(movie => movie.MovieScreenings.Any(ms => ms.StartTime.Date == date.Date));

            //Design patterns 
            //levi vivify, ktek
            //Ftn azure pretplata ili besplatni tier
            //Rad sa service buss, kafka etc.
            //Rad sa dockerima

            if (sortAlphabetically)
            {
                query = query.OrderBy(movie => movie.Name);
            }

            if (sortChronologically)
            {
                query = query.Include(movie => movie.MovieScreenings
                    .Where(movieScreening => movieScreening.StartTime.Date == date.Date)
                    .OrderBy(movieScreening => movieScreening.StartTime));
            }
            else
            {
                query = query.Include(movie => movie.MovieScreenings
                    .Where(movieScreening => movieScreening.StartTime.Date == date.Date));
            }
            var queryToString = query.ToQueryString();
            int numOfMovies = query.Count();
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return (await query.AsNoTracking().ToListAsync(), numOfMovies);
        }


        public async Task<(ICollection<Movie>,int)> GetAllFilteredAsync(
            ICollection<Char> firstLetters,
            string name = "",
            int pageNumber = 1, int pageSize = 10,
            CancellationToken cancellationToken = default
            )
        {
            var firstLetterStrings = firstLetters.Select(c => c.ToString()).ToList();

            int numOfLetters = firstLetters.Count();

            IQueryable<Movie> query = dbSet;

            switch (numOfLetters,name)
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

            if (pageNumber < 0 || pageSize < 0)
                throw new Exception("Page size or page number less than zero");
            int numOfMovies = query.Count();
            var a = query.ToQueryString();
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(m => m.MovieScreenings).Include(m => m.Genres);


            return (await query.ToListAsync(cancellationToken), numOfMovies);
        }
        public override async Task<Movie> GetbyIdAsync(Guid movieId, CancellationToken cancellationToken = default)
        {
            return await dbSet.Include(movie => movie.Genres).FirstAsync(movie => movie.Id == movieId);
        }

        public async Task<float> GetMovieScore(Guid movieId)
        {
            var reservations = dbContext.Set<Reservation>();
            var seats = dbContext.Set<Seat>();
            var moviesScrs = dbContext.Set<MovieScreening>();
            var query = from res in reservations
                        join seat in seats on res.Id equals seat.ReservationId
                        join movieScr in moviesScrs on seat.MovieScreeningId equals movieScr.Id
                        join movie in dbSet on movieScr.MovieId equals movie.Id
                        where movie.Id == movieId && res.Score != 0
                        select new
                        {
                            res.Score
                        };
            var score = query.Average(x => x.Score);

            return (float)score;
        }
    }
}
