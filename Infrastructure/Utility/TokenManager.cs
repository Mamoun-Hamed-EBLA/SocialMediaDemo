using Application.Models;
using ChallengeX.Infra.Data.Identity.Models;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Utility;

public class TokenManager : ITokenManager
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly JWTSettings _jwtSettings;

	public TokenManager(UserManager<ApplicationUser> userManager,
		 IOptions<JWTSettings> jwtOption)
	{
		_userManager = userManager;
		_jwtSettings = jwtOption.Value;
	}
	public JwtSecurityToken Decode(string token)
	{
		var handler = new JwtSecurityTokenHandler();
		return handler.ReadJwtToken(token);

	}
	public async Task<TokenInfo> GenerateToken(ApplicationUser user)
	{
		var claims = await GetUserClaims(user);
		var accessToken = GenerateAccessToken(claims);


		return new TokenInfo(accessToken);
	}

	private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
	{
		var roles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim>(
			[
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			]);

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		return claims;
	}
	private string GenerateAccessToken(List<Claim> claims)
	{
		var tokenHandler = new JwtSecurityTokenHandler();

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var expiredDate = DateTime.UtcNow.Add(_jwtSettings.AccessLifetime);

		var descriptor = new SecurityTokenDescriptor
		{
			SigningCredentials = credentials,
			Subject = new ClaimsIdentity(claims),
			Expires = expiredDate,
			Issuer = _jwtSettings.Issuer,
		};

		var token = tokenHandler.CreateToken(descriptor);

		return tokenHandler.WriteToken(token);
	}


}
