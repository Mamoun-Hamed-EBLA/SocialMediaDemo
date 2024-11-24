using Application.Models;
using ChallengeX.Infra.Data.Identity.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Utility
{
	public interface ITokenManager
	{
		JwtSecurityToken Decode(string token);
		Task<TokenInfo> GenerateToken(ApplicationUser user);
	}
}