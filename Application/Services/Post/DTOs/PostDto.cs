

using Application.Services.Comment.DTOs;

namespace Application.Services.Post.DTOs;
public class PostDto : BaseDto, IMapFrom<PostEntity>
{
	public Guid Id { get; set; }
	public string Post { get; set; } = string.Empty;

	public List<UserBriefDto> UsersLikes { get; set; } = new List<UserBriefDto>();


	public UserBriefDto User { get; set; } = new UserBriefDto();

	public CommentDto? FirstComment { get; set; }


	public void Mapping(Profile profile)
	{
		profile.CreateMap<PostEntity, PostDto>()
			.ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id.Value))
			.ForMember(dist => dist.UsersLikes, opt => opt.MapFrom(src => src.Likes.Select(e => e.User)))
			.ForMember(dist => dist.FirstComment, opt => opt.MapFrom(src => src.Comments.FirstOrDefault()))
			;
	}
}
