using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaAppContracts.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(List<Claim> authClaims);
    }
}
