using Domain.Base;

namespace Domain.Entities;
public class UserEntity : Entity<string>
{
	private readonly List<PostEntity> _posts = new List<PostEntity>();
	private readonly List<CommentEntity> _comments = new List<CommentEntity>();
	private readonly List<PostUserLikeEntity> _likes = new();

	private UserEntity(string userName)
	{
		UserName = userName;
	}
	public string UserName { get; set; }

	public static UserEntity Create(string userName) => new(userName);
	public static UserEntity Create(string id, string userName) => new(userName)
	{
		Id = id,
	};


	public IReadOnlyCollection<PostEntity> Post => _posts;
	public IReadOnlyCollection<CommentEntity> Comments => _comments;
	public IReadOnlyCollection<PostUserLikeEntity> Likes => _likes;

}
