using Application.Exceptions;
using Application.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
	public string? CompareTo { get; set; }

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{

		var apiKey = context.HttpContext.Request.Headers["Api-Key"];
		if (string.IsNullOrEmpty(apiKey))
			throw new UnauthorizedAccessException();

		var environmentSettingsSnapshot = context.HttpContext.RequestServices
			.GetRequiredService<IOptionsSnapshot<EnvironmentSettings>>();

		var compareTo = !string.IsNullOrEmpty(CompareTo)
			? CompareTo
			: environmentSettingsSnapshot.Value.ApiKey;

		if (apiKey != compareTo)
			throw new ForbiddenAccessException();

		await next();
	}
}