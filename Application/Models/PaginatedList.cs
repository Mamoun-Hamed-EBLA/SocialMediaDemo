using Microsoft.EntityFrameworkCore;

namespace Application.Models;

public class PaginatedList<T>
{
	public List<T> Items { get; }
	public int PageNumber { get; }
	public int TotalPages { get; }
	public int TotalItems { get; }

	public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		TotalItems = count;
		Items = items;
	}

	public bool HasPreviousPage => PageNumber > 1;

	public bool HasNextPage => PageNumber < TotalPages;

	public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
	{
		var count = await source.CountAsync();
		var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

		return new PaginatedList<T>(items, count, pageNumber, pageSize);
	}

	public static PaginatedList<T> CreateAsync(List<T> list, int pageNumber, int pageSize)
	{
		var count = list.Count();
		var items = list;

		return new PaginatedList<T>(items, count, pageNumber, pageSize);
	}

	public static PaginatedList<T> Empty => new PaginatedList<T>(new List<T>(), 0, 1, 10);

	public static PaginatedList<T> Create(IEnumerable<T> list, int pageNumber, int pageSize)
	{
		var count = list.Count();
		var items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		return new PaginatedList<T>(items, count, pageNumber, pageSize);
	}

}