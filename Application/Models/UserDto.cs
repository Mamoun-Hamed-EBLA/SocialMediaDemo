namespace Application.Models;
public class UserBriefDto : IMapFrom<UserEntity>
{
	public string Id { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;

}