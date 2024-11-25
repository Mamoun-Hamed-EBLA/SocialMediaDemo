using Application.Guards;

namespace Application.Services.Comment.Commands.Update;

public record UpdateCommentCommand(Guid Id, string Comment) : IRequest;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
	private readonly IDbContext _db;
	private readonly ICurrentUserService _currentUser;

	public UpdateCommentCommandHandler(IDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
	{
		var comment = await _db.Comments.FirstOrDefaultAsync(e => e.Id == new CommentId(request.Id)).ConfigureAwait(false);
		Guard.AgainstNull(comment);

		if (comment!.UserId != _currentUser.UserId)
		{
			throw new ApplicationException("You can't update others comments");
		}
		comment.Comment = request.Comment;

		await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
	}
}
