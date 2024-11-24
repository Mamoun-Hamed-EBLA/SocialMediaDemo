namespace Application.Exceptions;

public class ForbiddenAccessException : Exception
{
	public ForbiddenAccessException(string? message) : base(message)
	{
	}

	public ForbiddenAccessException() : base()
	{
	}

	public ForbiddenAccessException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}