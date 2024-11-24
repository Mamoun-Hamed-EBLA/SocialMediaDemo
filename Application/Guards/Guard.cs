namespace Application.Guards;
public static class Guard
{
	public static void AgainstNull<T>(T value)
	{
		if (value == null)
			throw new NotFoundException($"{typeof(T).Name} not found");
	}

	public static void AgainstNullOrEmptyCollection<T>(IEnumerable<T> value)
	{
		if (value == null)
			throw new NotFoundException($"{typeof(T).Name} not found");
		if (!value.Any())
		{
			throw new ArgumentException($"list : {typeof(T).Name} should not be empty");
		}
	}
	public static void CheckEffectedRecords<T>(int effectedRows)
	{
		if (effectedRows < 1)
			throw new NotFoundException($"{typeof(T).Name} not found");

	}
}
