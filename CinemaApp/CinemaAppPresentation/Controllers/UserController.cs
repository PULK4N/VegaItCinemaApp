using CinemaAppContracts.Request.UserRequests;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using CinemaAppContracts.DTO.UserDTOs;
using CinemaAppContracts.Wrappers;
using CinemaAppServices.Filters;

namespace CinemaAppPresentation.Controllers
{
    [ApiController]
    [Route("api/")]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
//localhost:7160/api/admin/users
        [HttpGet]
        [Route("admin/users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] UserFilter filter, CancellationToken cancellationToken)
        {
            var userFilter = _mapper.Map<ServiceUserFilter>(filter);
            var usersWithCount = await _userService.GetAllAsync(userFilter);
            List <UserForReturningDTO> userForReturningDTOs = _mapper.Map<List<UserForReturningDTO>>(usersWithCount.Item1);
            var pagedResponse = new PagedResponse<List<UserForReturningDTO>>(userForReturningDTOs
                , usersWithCount.Item2 / userFilter.PageSize + 1, userFilter.PageSize, (int)Math.Ceiling((double)(usersWithCount.Item2 / filter.PageSize)) + 1);
            return Ok(pagedResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/users/{userId:guid}")]
        public async Task<IActionResult> GetById(Guid userId, CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetByIdAsync(userId));
        }
        //api/admin/users/block/cc584900-dd5a-444b-64d3-08da66599318
        [Route("admin/users/block/{userId:guid}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> BlockUser(Guid userId, CancellationToken cancellationToken)
        {
            await _userService.BlockUserAsync(userId);
            return Ok();
        }
        [Route("admin/users/unblock/{userId:guid}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UnBlockUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            await _userService.UnBlockUserAsync(userId);
            return Ok();
        }

        [Route("users/register")]
        [AllowAnonymous]
        [HttpPost]
        [EnableCors]
        public async Task<IActionResult> Register(UserRegisterDTO userDTO)
        {
            return Ok(await _userService.RegisterUserAsync(_mapper.Map<UserRegisterRequest>(userDTO)));
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("users/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            var tokenWithRoles = await _userService.LoginUserAsync(_mapper.Map<UserLoginRequest>(model));
            if(tokenWithRoles.Item1 is not null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(tokenWithRoles.Item1),
                    roles = tokenWithRoles.Item2.ToList(),
                    expiration = tokenWithRoles.Item1.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpDelete("admin/users/{userId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteAsync(userId);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("users/get-reset-link")]
        public async Task<IActionResult> RestartPassword([FromBody] UserEmailDTO userEmailDTO)
        {
            await _userService.SendResetPasswordLink(userEmailDTO.Email);
            return Ok("Email with password reset link has been sent to user");
        }

        [Route("users/reset-password")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] UserForResetPasswordDTO userDTO)
        {
            return Ok(await _userService.ResetPassword(userDTO.Email, userDTO.Token, userDTO.Password));
        }

        [Route("users/confirm")]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmDTO emailConfirmDTO)
        {
            return Ok(await _userService.ConfirmEmail(emailConfirmDTO.Email, emailConfirmDTO.Token));
        }
    }
}
