using CinemaAppContracts.Request;
using CinemaAppContracts.Request.UserRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppServices.Entities;
using CinemaAppServices.Exceptions;
using CinemaAppServices.IRepositories;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemaAppServices.Exceptions.User;
using AutoMapper;
using CinemaAppServices.Filters;

namespace CinemaAppContracts.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IEmailService emailService, ITokenService tokenService, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task BlockUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);
            user.IsBlocked = true;
            await _userManager.UpdateAsync(user);
        }
        public async Task UnBlockUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);
            user.IsBlocked = false;
            await _userManager.UpdateAsync(user);
        }

        public async Task<UserResponse> RegisterUserAsync(UserRegisterRequest userRegisterRequest, CancellationToken cancellationToken = default)
        {
            UserResponse userResponse = await CreateAsync(userRegisterRequest);//, user.PasswordHash
            return userResponse;
        }
        public async Task<UserResponse> CreateAsync(UserRegisterRequest userDTO, CancellationToken cancellationToken = default)
        {
            User identityUser = new User(userDTO.Username, userDTO.Email, userDTO.DateOfBirth);
            var result = await _userManager.CreateAsync(identityUser, userDTO.Password);//, user.PasswordHash
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                _emailService.SendEmailConfirmationToken(identityUser.Email,token);
                return _mapper.Map<UserResponse>(identityUser);
            }
            throw new UserRegistrationFailException(result.Errors.ToString());
        }

        public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return;
            }
            throw new UserNotFoundException(userId);
        }

        public async Task<UserResponse> GetByEmailAsync(string email)
        {
            User user = (await _userManager.FindByEmailAsync(email));
            if (user is null)
                throw new UserNotFoundException(email);
            return _mapper.Map<UserResponse>(user);
        }
        public async Task<UserResponse> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            User user = await _userManager.FindByIdAsync(guid.ToString());
            if (user is null)
                throw new UserNotFoundException(guid);
            return _mapper.Map<UserResponse>(user);
        }
        public async Task<UserResponse> GetByNameAsync(string username)
        {
            User user = await _userManager.FindByNameAsync(username);
            if(user is null)
                throw new UserNotFoundException(username);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<(ICollection<UserResponse>,int)> GetAllAsync(ServiceUserFilter filter, CancellationToken cancellationToken = default)
        {
            var usersWithCount = await _unitOfWork.UserRepository.GetAllFilteredAsync(filter.FirstLetters,filter.Username,filter.PageNumber,filter.PageSize,cancellationToken);
             
            return (_mapper.Map<ICollection<UserResponse>>(usersWithCount.Item1), usersWithCount.Item2);
        }


        public async Task<(JwtSecurityToken,IList<string>)> LoginUserAsync(UserLoginRequest userLoginRequest, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByNameAsync(userLoginRequest.EmailOrUsername);
            if (user is null)
                user = await _userManager.FindByEmailAsync(userLoginRequest.EmailOrUsername);

            if (user is null)
                throw new UserNotFoundException(userLoginRequest.EmailOrUsername);

            if (user.IsBlocked)
                throw new UserIsBlockedException(user.Id);

            if (user.EmailConfirmed == false)
                throw new UserEmailIsNotConfirmedException();
            if (await _userManager.CheckPasswordAsync(user, userLoginRequest.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                JwtSecurityToken token = _tokenService.GenerateToken(authClaims);

                return (token,userRoles);
            }
            throw new BadPasswordException();
        }

        public async Task MakeAdminAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if(user is null)
                throw new UserNotFoundException(userId);

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if(result.Succeeded == false)
            {
                throw new CanNotAddRoleException("Admin");
            }
        }


        public async Task UpdateAsync(Guid userId, UserUpdateRequest userUpdateRequest, CancellationToken cancellationToken = default)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException(userId);
            user.DateOfBirth = userUpdateRequest.DateOfBirth;
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userManager.UpdateNormalizedUserNameAsync(user);
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserResponse> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UserNotFoundException(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return _mapper.Map<UserResponse>(user);
            }
            else
            {
                throw new EmailConfirmFailException();
            }
        }

        public async Task SendResetPasswordLink(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNotFoundException(email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _emailService.SendEmailResetPasswordLink(email, token);
        }

        
        public async Task<UserResponse> ResetPassword(string email, string token, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return _mapper.Map<UserResponse>(user);
            }
            throw new PasswordResetFailException();
            
        }
    }
}
