namespace Application.Models;

public record BaseQuery : AbstractQuery
{
	public DateTime? MinCreatedAt { get; set; }
	public DateTime? MaxCreatedAt { get; set; }

	public DateTime? MinLastModified { get; set; }
	public DateTime? MaxLastModified { get; set; }

	public string? OrderValue { get; set; }

	public bool? isDescending { get; set; }
}

public abstract record AbstractQuery
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
}