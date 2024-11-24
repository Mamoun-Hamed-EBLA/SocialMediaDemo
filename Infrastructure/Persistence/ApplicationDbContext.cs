using Application.Interfaces;
using ChallengeX.Infra.Data.Identity.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
{
	protected readonly IDateTime DateTime;


	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options,
		IDateTime dateTime) : base(options)
	{
		DateTime = dateTime;
	}
	public DbSet<UserEntity> DomainUsers { get; set; }
	public DbSet<PostEntity> Posts { get; set; }
	public DbSet<CommentEntity> Comments { get; set; }

	public DbSet<PostUserLikeEntity> Likes { get; set; }



	private void PopulateDomainUsers()
	{
		foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
		{
			if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
			{
				PopulateUserEntity(entry.Entity);
			}
		}
	}

	private static void PopulateUserEntity(ApplicationUser identityUser)
	{
		var userEntity = identityUser.User ?? UserEntity.Create(identityUser.Id, identityUser.UserName!);
		identityUser.User = userEntity;
	}
	private void SetAuditableProperties()
	{
		foreach (var entry in ChangeTracker.Entries<IAudibleEntity>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.SetCreated(DateTime.Now);
					break;

				case EntityState.Modified:
					entry.Entity.SetLastModified(DateTime.Now);
					break;
			}
		}
	}
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		PopulateDomainUsers();
		SetAuditableProperties();
		var result = await base.SaveChangesAsync(cancellationToken);

		return result;
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		base.OnModelCreating(builder);
	}
}