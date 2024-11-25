using Application.Guards;

namespace Application.Services.Comment.Commands.Delete;

public record DeleteCommentCommand(Guid Id) : IRequest;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
	private readonly IDbContext _db;
	private readonly ICurrentUserService _currentUser;

	public DeleteCommentCommandHandler(IDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
	{
		var comment = await _db.Comments.FirstOrDefaultAsync(e => e.Id == new CommentId(request.Id)).ConfigureAwait(false);
		Guard.AgainstNull(comment);

		if (comment!.UserId != _currentUser.UserId)
		{
			throw new ApplicationException("You can't delete others comments");
		}
		_db.Comments.Remove(comment);
		await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}
}