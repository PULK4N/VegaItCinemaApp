using CinemaAppContracts.DTO;
using CinemaAppContracts.DTO.UserDTOs;
using CinemaAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface IUserService : IEntityService<User, UserRegisterDTO, UserForUpdatingDTO, UserForReturningDTO, CinemaAppContracts.Wrappers.UserFilter>
    {
        Task<UserForReturningDTO> RegisterUserAsync(UserRegisterDTO userDTO, CancellationToken cancellationToken = default);
        Task<JwtSecurityToken> LoginUserAsync(UserLoginDTO userDTO, CancellationToken cancellationToken = default);
        Task BlockUserAsync(Guid userId,CancellationToken cancellationToken = default);
        Task UnBlockUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task VerifyUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task MakeAdminAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<UserForReturningDTO> GetByNameAsync(string username);
        Task<UserForReturningDTO> GetByEmailAsync(string email);
        Task<UserForReturningDTO> ConfirmEmail(string email, string token);
        Task SendResetPasswordLink(string user);
        Task<UserForReturningDTO> ResetPassword(string email, string token, string password);
    }
}
