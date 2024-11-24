using Application.Guards;

namespace Application.Services.Post.Commands.Like;

public record LikePostCommand(Guid PostId) : IRequest;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand>
{
	private readonly IDbContext _db;
	private readonly ICurrentUserService _currentUser;

	public LikePostCommandHandler(IDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	public async Task Handle(LikePostCommand request, CancellationToken cancellationToken)
	{
		var post = await _db.Posts
			.Include(e => e.Likes.Where(l => l.UserId == _currentUser.UserId))
			.FirstOrDefaultAsync(e => e.Id == new PostId(request.PostId), cancellationToken);
		Guard.AgainstNull(post);

		if (post!.Likes.Any())
		{
			throw new ApplicationException("this post already liked by you");
		}
		post!.AddLike(_currentUser.UserId);

		await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

	}
}