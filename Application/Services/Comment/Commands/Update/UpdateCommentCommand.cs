using Application.Guards;
using FluentValidation;

namespace Application.Services.Comment.Commands.Update;

public record UpdateCommentCommand(Guid Id, string Comment) : IRequest;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
	private readonly IDbContext _db;

	public UpdateCommentCommandHandler(IDbContext db)
	{
		_db = db;
	}

	public async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
	{
		var updatedRecords = await _db.Comments.Where(e => e.Id == new CommentId(request.Id))
			.ExecuteUpdateAsync(
			entity => entity.SetProperty(e => e.Comment, request.Comment),
			cancellationToken)
			.ConfigureAwait(false);

		Guard.CheckEffectedRecords<CommentEntity>(updatedRecords);
	}
}
