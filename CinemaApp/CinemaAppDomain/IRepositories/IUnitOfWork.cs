using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.IRepositories
{
    public interface IUnitOfWork
    {
        IGenreRepository GenreRepository { get; }
        IMovieRepository MovieRepository { get; }
        IMovieScreeningRepository MovieScreeningRepository { get; }
        ISeatRepository SeatRepository { get; }
        IUserRepository UserRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IImageRepository ImageRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
