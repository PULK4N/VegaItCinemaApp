using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.IRepositories
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<ICollection<Reservation>> GetReservationsByMovieScreeningId(Guid movieScreeningId);
    }
}
