using Domain.Base;
using Domain.StrongTypes;

namespace Domain.Entities;
public class CommentEntity : Entity<CommentId>
{
	private CommentEntity(string userId, PostId postId, CommentId id, string comment)
	{
		Id = id;
		UserId = userId;
		PostId = postId;
		Comment = comment;
	}
	public string Comment { get; set; }

	public PostId PostId { get; set; }

	public PostEntity? Post { get; set; }

	public string UserId { get; set; }

	public UserEntity? User { get; set; }
	public static CommentEntity Create(string userId, PostId postId, CommentId id, string comment) => new(userId, postId, id, comment);
}
