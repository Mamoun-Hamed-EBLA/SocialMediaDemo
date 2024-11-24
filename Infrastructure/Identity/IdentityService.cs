using Application.Guards;
using Application.Interfaces;
using Application.Models;
using ChallengeX.Infra.Data.Identity.Models;
using Domain.Entities;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Identity;

namespace ChallengeX.Infra.Data.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITokenManager _tokenManager;

		public IdentityService(
			UserManager<ApplicationUser> userManager,
			ITokenManager tokenManager)
		{
			_userManager = userManager;
			_tokenManager = tokenManager;

		}

		public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
		{


			return await CreateUser(user, password);
		}

		private async Task<IdentityResult> CreateUser(UserEntity domainUser, string password)
		{
			var identityUser = FromDomainUser(domainUser);

			var result = await _userManager.CreateAsync(identityUser, password);

			return result;
		}

		private ApplicationUser FromDomainUser(UserEntity domainUser)
		{
			return new ApplicationUser
			{
				UserName = domainUser.UserName,
				User = domainUser,
			};
		}


		public async Task<TokenInfo> SignInAsync(string useName, string password)
		{
			var user = await _userManager.FindByNameAsync(useName);

			Guard.AgainstNull(user);

			return await SignIn(user!, password);
		}

		private async Task<TokenInfo> SignIn(ApplicationUser user, string password)
		{
			var success = await _userManager.CheckPasswordAsync(user, password);

			if (!success) throw new UnauthorizedAccessException();

			return await _tokenManager.GenerateToken(user);
		}
	}
}