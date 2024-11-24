using AutoMapper;

namespace Application.Services.Comment.DTOs;
public class CommentDto : BaseDto, IMapFrom<CommentEntity>
{
	public Guid Id { get; set; }
	public Guid PostId { get; set; }

	public string Comment { get; set; } = string.Empty;


	public void Mapping(Profile profile)
	{
		profile.CreateMap<CommentEntity, CommentDto>()
			.ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id.Value))
			.ForMember(dist => dist.PostId, opt => opt.MapFrom(src => src.PostId.Value))
			;
	}
}
