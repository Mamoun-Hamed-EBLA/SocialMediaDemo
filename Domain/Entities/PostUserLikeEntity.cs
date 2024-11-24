using Domain.StrongTypes;

namespace Domain.Entities;
public class PostUserLikeEntity
{
	private PostUserLikeEntity(string userId, PostId postId)
	{
		UserId = userId;
		PostId = postId;
	}

	public static PostUserLikeEntity Create(string userId, PostId postId) => new(userId, postId);
	public string UserId { get; set; }

	public UserEntity? User { get; set; }

	public PostId PostId { get; set; }

	public PostEntity? Post { get; set; }
}
