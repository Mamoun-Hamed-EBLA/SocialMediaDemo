using API.Models;

namespace Shared.Models;

public class FailObject
{
    public bool Success => false;

    public object Data { get; set; }

    public ErrorDetails Error { get; set; }

    public FailObject(ErrorDetails error)
    {
        Data = new { };
        Error = error;
    }

    public static FailObject? Initialize(ErrorDetails error)
    {
        return new FailObject(error);
    }
}

public class FailObject<T> where T : new()
{
    public FailObject(ErrorDetails error)
    {
        Data = new T();
        Error = error;
    }

    public bool Success => false;

    public T Data { get; set; }

    public ErrorDetails Error { get; set; }
}