namespace API.Models;

public class ErrorDetails
{
	public string Type { get; set; } = "";
	public string Code { get; set; } = "";

	public List<string> Errors { get; set; } = new List<string>();

	public ErrorDetails(string type, string code, Exception? exception)
	{
		Type = type;
		Code = code;
		if (exception?.Message != null)
		{
			Errors.Add(exception.Message);
		}
		while (exception?.InnerException != null)
		{
			Errors.Add(exception.InnerException.Message);
			exception = exception.InnerException;
		}
	}

	public ErrorDetails(string type, string code, IDictionary<string, string[]> errors)
	{
		Type = type;
		Code = code;
		foreach (var error in errors)
		{
			foreach (var detail in error.Value)
			{
				Errors.Add(detail);
			}
		}
	}

	public ErrorDetails(string type, string code, List<string> errors)
	{
		Type = type;
		Code = code;
		Errors = errors;
	}
}