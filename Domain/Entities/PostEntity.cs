using Domain.Base;
using Domain.StrongTypes;

namespace Domain.Entities;
public class PostEntity : Entity<PostId>
{
	private readonly List<CommentEntity> _comments = new();
	private readonly List<PostUserLikeEntity> _likes = new();
	private PostEntity(string userId, PostId id, string post)
	{
		Id = id;
		Post = post;
		UserId = userId;
	}

	public static PostEntity Create(string userId, PostId id, string post) => new(userId, id, post);

	public string Post { get; private set; }

	public string UserId { get; set; }

	public UserEntity? User { get; set; }
	public IReadOnlyCollection<CommentEntity> Comments => _comments;
	public IReadOnlyCollection<PostUserLikeEntity> Likes => _likes;



	public void UpdatePost(string post)
	{
		Post = post;
	}
	public void AddLike(string userId)
	{
		_likes.Add(PostUserLikeEntity.Create(userId, Id));
	}

	public void AddComment(string userId, string comment)
	{

		_comments.Add(CommentEntity.Create(userId, Id, new CommentId(Guid.NewGuid()), comment));
	}


}
