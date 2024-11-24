using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;
public class PostUserLikeConfiguration : IEntityTypeConfiguration<PostUserLikeEntity>
{
	public void Configure(EntityTypeBuilder<PostUserLikeEntity> builder)
	{
		builder.HasKey(e => new { e.UserId, e.PostId });

		builder.HasOne(e => e.User)
			   .WithMany(e => e.Likes)
			   .HasForeignKey(e => e.UserId)
			   .OnDelete(DeleteBehavior.NoAction);


		builder.HasOne(e => e.Post)
			.WithMany(e => e.Likes)
			.HasForeignKey(e => e.PostId);
	}
}
