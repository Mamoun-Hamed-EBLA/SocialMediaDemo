using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;
public interface IIdentityService
{
	Task<IdentityResult> CreateUserAsync(UserEntity user, string password);
	Task<TokenInfo> SignInAsync(string userName, string password);
}