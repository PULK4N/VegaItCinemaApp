using CinemaAppContracts.DTO.MovieScreeningDTOs;
using CinemaAppContracts.Wrappers;
using CinemaAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface IMovieScreeningService : IEntityService<MovieScreening, MovieScreeningForCreatingDTO, MovieScreeningForUpdatingDTO, MovieScreeningForReturningDTO, MovieScreeningFilter>
    {
    }
}
