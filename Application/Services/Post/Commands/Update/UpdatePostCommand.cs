using Application.Guards;

namespace Application.Services.Post.Commands.Update;

public record UpdatePostCommand(Guid Id, string Post) : IRequest;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
	private readonly IDbContext _db;

	public UpdatePostCommandHandler(IDbContext db)
	{
		_db = db;
	}

	public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
	{
		var updatedRecords = await _db.Posts.Where(e => e.Id == new PostId(request.Id))
			.ExecuteUpdateAsync(
			entity => entity.SetProperty(e => e.Post, request.Post),
			cancellationToken)
			.ConfigureAwait(false);

		Guard.CheckEffectedRecords<PostEntity>(updatedRecords);
	}
}