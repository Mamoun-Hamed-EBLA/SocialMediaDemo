using Application.Guards;

namespace Application.Services.Comment.Commands.Delete;

public record DeleteCommentCommand(Guid Id) : IRequest;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
	private readonly IDbContext _db;

	public DeleteCommentCommandHandler(IDbContext db)
	{
		_db = db;
	}

	public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
	{
		var deletedRecords = await _db.Comments.Where(e => e.Id == new CommentId(request.Id))
			 .ExecuteDeleteAsync(cancellationToken)
			 .ConfigureAwait(false);

		Guard.CheckEffectedRecords<CommentEntity>(deletedRecords);
	}
}