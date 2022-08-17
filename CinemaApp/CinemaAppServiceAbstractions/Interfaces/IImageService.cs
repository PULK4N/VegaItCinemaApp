using CinemaAppDomain.Entities;
using CinemaAppContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface IImageService
    {
        Task<ImageDTO> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}
