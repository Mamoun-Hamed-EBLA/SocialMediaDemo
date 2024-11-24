namespace API.Models;

public class SuccessObject
{
	public bool Success => true;

	public object Data { get; set; }

	public object Error { get; set; }

	public SuccessObject(object? value)
	{
		Data = value ?? new { };
		Error = new { };
	}
}

public class SuccessObject<T>
{
	public SuccessObject(T data, object error)
	{
		Success = true;
		Data = data;
		Error = error;
	}

	public bool Success { get; }

	public T Data { get; set; }

	public object Error { get; set; }
}