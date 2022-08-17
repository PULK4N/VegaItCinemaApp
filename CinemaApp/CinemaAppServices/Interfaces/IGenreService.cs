using CinemaAppContracts.Request.GenreRequests;
using CinemaAppServices.Entities;
using CinemaAppServices.Filters;

namespace CinemaAppContracts.Interfaces
{
    public interface IGenreService : IEntityService<Genre, GenreCreateRequest, GenreUpdateRequest, GenreResponse, ServiceGenreFilter>
    {
    }
}
