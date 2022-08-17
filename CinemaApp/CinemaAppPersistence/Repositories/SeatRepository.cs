using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;

namespace CinemaAppPersistence.Repositories
{
    internal class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        public SeatRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
        }
    }
}