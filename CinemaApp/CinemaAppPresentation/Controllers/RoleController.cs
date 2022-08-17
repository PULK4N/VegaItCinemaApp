using CinemaAppContracts.Request;
using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppPresentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;
        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/roles")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(_roleManager.Roles.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("admin/roles/add-to-user/")]
        public async Task<IActionResult> AddRoleToUser([FromBody] UserRoleDTO userRoleDto)
        {
            var user = await _userManager.FindByNameAsync(userRoleDto.UserName);
            var role = await _roleManager.FindByNameAsync(userRoleDto.RoleName);

            if (user is not null && role is not null)
            {
                await _userManager.AddToRoleAsync(user, userRoleDto.RoleName);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/roles/get-user-roles/{userId:guid}")]
        public async Task<IActionResult> GetUserRoles(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);

            if (user is not null && roles is not null)
            {
                return Ok(roles);
            }
            return BadRequest();
        }
    }
}
