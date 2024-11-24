using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.Extensions;

public static class QueryExtensions
{
	public static IQueryable<T> ApplyBaseFilter<T>(this IQueryable<T> query, BaseQuery filter) where T : IAudibleEntity
	{
		if (filter.MinCreatedAt is not null)
			query = query.Where(x => x.Created >= filter.MinCreatedAt);
		if (filter.MaxCreatedAt is not null)
			query = query.Where(x => x.Created <= filter.MaxCreatedAt);

		if (filter.MinLastModified is not null)
			query = query.Where(x => x.LastModified! >= filter.MinLastModified);
		if (filter.MaxLastModified is not null)
			query = query.Where(x => x.LastModified! <= filter.MaxLastModified);

		return query;
	}

	public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
	{
		if (condition)
		{
			return query.Where(predicate);
		}
		return query;
	}

	public static IQueryable<T> OrderIf<T>(this IQueryable<T> query,
										   bool condition,
										   Expression<Func<T, int>> orderExpression
										  )
	{
		if (condition)
		{
			return query.OrderBy(orderExpression);
		}
		return query;
	}
	public static IQueryable<T> OrderByDescendingIf<T>(this IQueryable<T> query,
										   bool condition,
										   Expression<Func<T, int>> orderExpression
										  )
	{
		if (condition)
		{
			return query.OrderByDescending(orderExpression);
		}
		return query;
	}

	public static IQueryable<T> ApplyOrdering<T, TKey>(this IQueryable<T> query,
		Dictionary<string, Expression<Func<T, TKey>>> allowedKeys, string? orderValue, bool? isDescending)
	{
		if (orderValue is null)
			return query;

		var success = allowedKeys.TryGetValue(orderValue, out var keySelector);
		if (!success)
		{
			throw new ApplicationException("invalid order value");
		}
		query = isDescending switch
		{
			null => query.OrderBy(keySelector!),
			false => query.OrderBy(keySelector!),
			true => query.OrderByDescending(keySelector!),

		};
		return query;
	}
}