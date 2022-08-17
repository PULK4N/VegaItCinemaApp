using CinemaAppServices.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppPersistence.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryDbContext _dbContext;
        private readonly Lazy<IGenreRepository> _lazyGenreRepository;
        private readonly Lazy<IMovieScreeningRepository> _lazyMovieScreeningRepository;
        private readonly Lazy<IMovieRepository> _lazyMovieRepository;
        private readonly Lazy<ISeatRepository> _lazySeatRepository;
        private readonly Lazy<IReservationRepository> _lazyTicketRepository;
        private readonly Lazy<IUserRepository> _lazyUserRepository;
        private readonly Lazy<IImageRepository> _lazyImageRepository;

        public UnitOfWork(RepositoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _lazyGenreRepository = new Lazy<IGenreRepository>(() => new GenreRepository(dbContext));
            _lazyMovieScreeningRepository = new Lazy<IMovieScreeningRepository>(() => new MovieScreeningRepository(dbContext));
            _lazyMovieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(dbContext));
            _lazySeatRepository = new Lazy<ISeatRepository>(() => new SeatRepository(dbContext));
            _lazyTicketRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(dbContext));
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContext));
            _lazyImageRepository = new Lazy<IImageRepository>(() => new ImageRepository(dbContext));
        }

        public IGenreRepository GenreRepository => _lazyGenreRepository.Value;

        public IMovieRepository MovieRepository => _lazyMovieRepository.Value;

        public IMovieScreeningRepository MovieScreeningRepository => _lazyMovieScreeningRepository.Value;

        public ISeatRepository SeatRepository => _lazySeatRepository.Value;

        public IReservationRepository ReservationRepository => _lazyTicketRepository.Value;

        public IUserRepository UserRepository => _lazyUserRepository.Value;

        public IImageRepository ImageRepository => _lazyImageRepository.Value;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
