using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChallengeX.Infra.Data.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public UserEntity? User { get; set; }

		//public ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();


	}
}