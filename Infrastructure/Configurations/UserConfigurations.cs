using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;
public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasMaxLength(450);
		;

		builder.Property(x => x.UserName)
		   .HasMaxLength(64)
		   .IsRequired();

		builder.ToTable("Users");

	}
}
