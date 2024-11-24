using ChallengeX.Infra.Data.Identity.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeX.Infra.Data.Configurations.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(e => e.Id)
			.ValueGeneratedOnAdd();

		builder.Property(e => e.UserName)
			.HasColumnName(nameof(ApplicationUser.UserName))
			.HasMaxLength(256);

		builder.Property(e => e.Email)
			.HasColumnName(nameof(ApplicationUser.Email))
			.HasMaxLength(256);

		builder.Property(e => e.NormalizedEmail)
			.HasColumnName(nameof(ApplicationUser.NormalizedEmail))
			.HasMaxLength(256);

		builder.Property(e => e.EmailConfirmed)
			.HasColumnName(nameof(ApplicationUser.EmailConfirmed));

		builder.Property(e => e.PhoneNumber)
			.HasColumnName(nameof(ApplicationUser.PhoneNumber))
			.HasMaxLength(450);

		builder.Property(e => e.PhoneNumberConfirmed)
			.HasColumnName(nameof(ApplicationUser.PhoneNumberConfirmed));

		builder.HasOne(entity => entity.User)
			.WithOne()
			.HasForeignKey<UserEntity>(entity => entity.Id);

		builder.ToTable("Users");

	}
}