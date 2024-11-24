namespace Application.Services.Post.Commands.Create;

public record CreatePostCommand(string Post) : IRequest<PostId>;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostId>
{
	private readonly IDbContext _db;
	private readonly ICurrentUserService _currentUser;

	public CreatePostCommandHandler(IDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}



	public async Task<PostId> Handle(CreatePostCommand request, CancellationToken cancellationToken)
	{
		var entity = PostEntity.Create(_currentUser.UserId!, new PostId(Guid.NewGuid()), request.Post);

		await _db.Posts.AddAsync(entity, cancellationToken).ConfigureAwait(false);

		await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		return entity.Id;

	}
}
