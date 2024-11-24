using Application.Interfaces;
using System.Security.Claims;

namespace API.Services;

public class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

	public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims
		.Where(c => c.Type == ClaimTypes.Role)
		.Select(c => c.Value) ?? new List<string>();
}