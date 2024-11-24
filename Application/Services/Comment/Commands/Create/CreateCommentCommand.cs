using Application.Guards;

namespace Application.Services.Comment.Commands.Create;

public record CreateCommentCommand(Guid PostId, string Comment) : IRequest;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
	private readonly IDbContext _db;
	private readonly ICurrentUserService _currentUser;

	public CreateCommentCommandHandler(IDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
	{
		var post = await _db.Posts.FirstOrDefaultAsync(e => e.Id == new PostId(request.PostId))
			.ConfigureAwait(false);
		Guard.AgainstNull(post);
		post!.AddComment(_currentUser.UserId, request.Comment);

		await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

	}
}
