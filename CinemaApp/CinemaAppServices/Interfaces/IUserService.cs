using CinemaAppContracts.Request;
using CinemaAppContracts.Request.UserRequests;
using CinemaAppServices.Entities;
using CinemaAppServices.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Interfaces
{
    public interface IUserService : IEntityService<User, UserRegisterRequest, UserUpdateRequest, UserResponse, ServiceUserFilter>
    {
        Task<UserResponse> RegisterUserAsync(UserRegisterRequest userDTO, CancellationToken cancellationToken = default);
        Task<(JwtSecurityToken, IList<string>)> LoginUserAsync(UserLoginRequest userDTO, CancellationToken cancellationToken = default);
        Task BlockUserAsync(Guid userId,CancellationToken cancellationToken = default);
        Task UnBlockUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task MakeAdminAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<UserResponse> GetByNameAsync(string username);
        Task<UserResponse> GetByEmailAsync(string email);
        Task<UserResponse> ConfirmEmail(string email, string token);
        Task SendResetPasswordLink(string user);
        Task<UserResponse> ResetPassword(string email, string token, string password);
    }
}
