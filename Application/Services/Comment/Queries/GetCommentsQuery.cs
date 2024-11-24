using Application.Extensions;
using Application.Services.Comment.DTOs;
using System.Linq.Expressions;


namespace Application.Services.Comment.Queries;
public record GetCommentsQuery : BaseQuery, IRequest<PaginatedList<CommentDto>>
{
	public Guid? Id { get; set; }
	public Guid? PostId { get; set; }

	public string? UserId { get; set; }

}


public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, PaginatedList<CommentDto>>
{
	private readonly IDbContext _db;
	private readonly IMapper _mapper;

	public GetCommentsQueryHandler(IDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}

	public async Task<PaginatedList<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
	{
		var allowedKeys = new Dictionary<string, Expression<Func<CommentEntity, object>>>()
		{
			{"created",e=>e.Created},
		};
		IQueryable<CommentEntity> query = BuildQuery(request, allowedKeys);

		return await query.ProjectTo<CommentDto>(_mapper.ConfigurationProvider, cancellationToken)
				  .PaginatedListAsync(request.PageNumber, request.PageSize).ConfigureAwait(false);
		;
	}

	private IQueryable<CommentEntity> BuildQuery(GetCommentsQuery request, Dictionary<string, Expression<Func<CommentEntity, object>>> allowedKeys)
	{
		return _db.Comments.AsNoTracking()
			.ApplyBaseFilter(request)
			.WhereIf(request.Id.HasValue, e => e.Id == new CommentId(request.Id!.Value))
			.WhereIf(request.PostId.HasValue, e => e.PostId == new PostId(request.PostId!.Value))
			.WhereIf(!string.IsNullOrEmpty(request.UserId), e => e.UserId == e.UserId)
			.ApplyOrdering(allowedKeys, request.OrderValue, request.isDescending);
	}
}
