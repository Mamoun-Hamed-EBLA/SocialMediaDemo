using Application.Guards;

namespace Application.Services.Post.Commands.Unlike;

public record UnlikePostCommand(Guid PostId) : IRequest;

public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand>
{
	private readonly IDbContext _db;
	private readonly ICurrentUserService _currentUser;

	public UnlikePostCommandHandler(IDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	public async Task Handle(UnlikePostCommand request, CancellationToken cancellationToken)
	{
		var deletedRecords = await _db.Likes.Where(e => e.PostId == new PostId(request.PostId) && e.UserId == _currentUser.UserId)
			 .ExecuteDeleteAsync(cancellationToken)
			 .ConfigureAwait(false);

		Guard.CheckEffectedRecords<CommentEntity>(deletedRecords);
	}
}