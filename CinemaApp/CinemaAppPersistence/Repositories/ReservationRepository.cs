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
    internal sealed class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context)
        {
        }
        public override async Task<Reservation> GetbyIdAsync(Guid reservationId, CancellationToken cancellationToken = default)
        {
            return await dbSet.Include(r => r.Seats).Where(r => r.Id == reservationId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Reservation>> GetReservationsByMovieScreeningId(Guid movieScreeningId)
        {
            var seats = dbContext.Set<Seat>();
            var movieScreenings = dbContext.Set<MovieScreening>();
            var query = from res in dbSet
                        join seat in seats on res.Id equals seat.ReservationId
                        join movieScr in movieScreenings on seat.MovieScreeningId equals movieScr.Id
                        where movieScr.Id == movieScreeningId && res.Score != 0
                        select new Reservation
                        {
                            Id = res.Id,
                            Price = res.Price,
                            Score = res.Score,
                            Seats = res.Seats,
                            TimeOfBuying = res.TimeOfBuying,
                            UserId = res.UserId
                        };

            return await query.ToListAsync();
        }
    }
}
