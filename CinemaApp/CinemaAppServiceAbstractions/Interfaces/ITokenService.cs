using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(List<Claim> authClaims);
    }
}
