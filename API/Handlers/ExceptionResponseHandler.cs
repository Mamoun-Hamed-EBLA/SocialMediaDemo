using API.Models;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Text.Json;

namespace API.Handlers;

public static class ExceptionResponseHandler
{
	private const string Application = "Bad Request";
	private const string Validation = "Validation Error";
	private const string Json = "JSON Serialization";
	private const string Unauthorized = "Unauthorized";
	private const string Forbidden = "Forbidden";
	private const string NotFound = "Not Found";
	private const string Unknown = "Unknown Exception";

	public static BadRequestObjectResult CreateBadRequestResponse(ApplicationException exception)
	{
		return new BadRequestObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7231#section-6.5.1",
					Application,
					exception)
				));
	}

	public static BadRequestObjectResult CreateBadJSONResponse(JsonException exception)
	{
		return new BadRequestObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7231#section-6.5.1",
					Json,
					new List<string>()
					{
						$"Failed to Serialize Request Object at Line: {exception?.LineNumber}," ,
						$"Path: {exception?.Path?.Replace("$.", "")}"
					})
				));
	}

	public static BadRequestObjectResult CreateValidationResponse(ValidationException exception)
	{
		return new BadRequestObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7231#section-6.5.1",
					Validation,
					exception.Errors)
				));
	}

	public static NotFoundObjectResult CreateNotFoundResponse(NotFoundException exception)
	{
		return new NotFoundObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7231#section-6.5.4",
					NotFound,
					exception)
				));
	}

	public static ObjectResult CreateUnauthorizedResponse(Exception exception)
	{
		return new ObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7235#section-3.1",
					Unauthorized,
					exception)
				))
		{
			StatusCode = StatusCodes.Status401Unauthorized
		};
	}

	public static ObjectResult CreateForbiddenResponse(Exception exception)
	{
		return new ObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7231#section-6.5.3",
					Forbidden,
					exception)
				))
		{
			StatusCode = StatusCodes.Status403Forbidden
		};
	}

	public static ObjectResult CreateUnknownResponse(Exception exception)
	{
		return new ObjectResult(
			FailObject.Initialize(
				new ErrorDetails(
					"https://tools.ietf.org/html/rfc7231#section-6.6.1",
					Unknown,
					exception)
				))
		{
			StatusCode = StatusCodes.Status500InternalServerError
		};
	}
}