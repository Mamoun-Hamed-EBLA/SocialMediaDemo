using Application.Extensions;
using Application.Services.Post.DTOs;
using System.Linq.Expressions;

namespace Application.Services.Post.Queries;
public record GetPostsQuery : BaseQuery, IRequest<PaginatedList<PostDto>>
{
	public Guid? Id { get; set; }

	public string? UserId { get; set; }
}


public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PaginatedList<PostDto>>
{
	private readonly IDbContext _db;
	private readonly IMapper _mapper;

	public GetPostsQueryHandler(IDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<PaginatedList<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
	{
		var allowedKeys = new Dictionary<string, Expression<Func<PostEntity, object>>>()
		{
			{"likes",e=>e.Likes } ,
			{"created",e=>e.Created},
		};
		IQueryable<PostEntity> query = BuildQuery(request, allowedKeys);

		return await query.ProjectTo<PostDto>(_mapper.ConfigurationProvider, cancellationToken)
				  .PaginatedListAsync(request.PageNumber, request.PageSize).ConfigureAwait(false);
		;
	}

	private IQueryable<PostEntity> BuildQuery(GetPostsQuery request, Dictionary<string, Expression<Func<PostEntity, object>>> allowedKeys)
	{
		return _db.Posts.AsNoTracking()
			.ApplyBaseFilter(request)
			.WhereIf(request.Id.HasValue, e => e.Id == new PostId(request.Id!.Value))
			.WhereIf(!string.IsNullOrEmpty(request.UserId), e => e.UserId == e.UserId)
			.ApplyOrdering(allowedKeys, request.OrderValue, request.isDescending);
	}
}
