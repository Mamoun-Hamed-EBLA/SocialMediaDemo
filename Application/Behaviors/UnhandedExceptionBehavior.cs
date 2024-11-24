using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;


public class UnhandedExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly ILogger<TRequest> _logger;
	public UnhandedExceptionBehavior(ILogger<TRequest> logger)
	{
		_logger = logger;
	}
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		try
		{
			return await next();
		}
		catch (Exception ex)
		{
			var requestName = typeof(TRequest).Name;
			_logger.LogError(ex, "CleanArchitecture Request: Unhanded Exception for Request {Name} {@Request}", requestName, request);


			throw;
		}
	}
}
