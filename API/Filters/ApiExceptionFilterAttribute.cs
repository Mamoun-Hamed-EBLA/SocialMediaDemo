using API.Handlers;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
	private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

	public ApiExceptionFilterAttribute()
	{
		// Register known exception types and handlers.
		_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
			{
				{typeof(ApplicationException), HandleApplicationException},
				{typeof(ValidationException), HandleValidationException},
				{typeof(NotFoundException), HandleNotFoundException},
				{typeof(UnauthorizedAccessException), HandleUnauthorizedRequestException},
				{typeof(ForbiddenAccessException), HandleForbiddenAccessException},
				{typeof(JsonException), HandleJsonException}
			};
	}

	public override void OnException(ExceptionContext context)
	{
		HandleException(context);
	}

	private void HandleException(ExceptionContext context)
	{
		var type = context.Exception.GetType();

		if (_exceptionHandlers.ContainsKey(type))
		{
			_exceptionHandlers[type].Invoke(context);
			return;
		}

		if (!context.ModelState.IsValid)
		{
			HandleInvalidModelStateException(context);
			return;
		}

		HandleUnknownException(context);
	}

	private static void HandleApplicationException(ExceptionContext context)
	{
		var exception = context.Exception as ApplicationException;

		context.Result = ExceptionResponseHandler.CreateBadRequestResponse(exception!);

		context.ExceptionHandled = true;
	}

	private static void HandleValidationException(ExceptionContext context)
	{
		var exception = context.Exception as ValidationException;

		context.Result = ExceptionResponseHandler.CreateValidationResponse(exception!);

		context.ExceptionHandled = true;
	}

	private static void HandleInvalidModelStateException(ExceptionContext context)
	{
		var details = new ValidationProblemDetails(context.ModelState)
		{
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
		};

		context.Result = new BadRequestObjectResult(details);

		context.ExceptionHandled = true;
	}

	private static void HandleNotFoundException(ExceptionContext context)
	{
		var exception = context.Exception as NotFoundException;

		context.Result = ExceptionResponseHandler.CreateNotFoundResponse(exception!);

		context.ExceptionHandled = true;
	}

	private static void HandleUnauthorizedRequestException(ExceptionContext context)
	{
		context.Result = ExceptionResponseHandler.CreateUnauthorizedResponse(context.Exception);

		context.ExceptionHandled = true;
	}

	private static void HandleForbiddenAccessException(ExceptionContext context)
	{
		context.Result = ExceptionResponseHandler.CreateForbiddenResponse(context.Exception);

		context.ExceptionHandled = true;
	}

	private static void HandleUnknownException(ExceptionContext context)
	{
		context.Result = ExceptionResponseHandler.CreateUnknownResponse(context.Exception);
		context.ExceptionHandled = true;
	}

	private static void HandleJsonException(ExceptionContext context)
	{
		var exception = context.Exception as JsonException;

		context.Result = ExceptionResponseHandler.CreateBadJSONResponse(exception!);

		context.ExceptionHandled = true;
	}
}