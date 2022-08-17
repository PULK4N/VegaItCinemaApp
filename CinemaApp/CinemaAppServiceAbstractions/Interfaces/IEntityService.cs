using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface IEntityService<T,TForCreatingDTO,TForUpdatingDTO, TForReturningDTO, TFilter>
    {
        Task<IEnumerable<TForReturningDTO>> GetAllAsync(TFilter filter, CancellationToken cancellationToken = default);
        Task<TForReturningDTO> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid guid, TForUpdatingDTO tDTO, CancellationToken cancellationToken = default);
        Task<TForReturningDTO> CreateAsync(TForCreatingDTO tDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}
