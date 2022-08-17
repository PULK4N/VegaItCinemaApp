using CinemaAppServices.Entities;
using CinemaAppContracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Interfaces
{
    public interface IImageService
    {
        Task<ImageRequest> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}
