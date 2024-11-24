using Domain.Entities;
using Domain.StrongTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;
public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
	public void Configure(EntityTypeBuilder<CommentEntity> builder)
	{
		builder.HasKey(x => x.Id)
			.IsClustered(false);

		builder.Property(x => x.Id)
			.HasMaxLength(450)
			.HasConversion(commentId => commentId.Value, id => new CommentId(id))
			;

		builder.Property(x => x.Comment)
		  .HasMaxLength(512)
		   .IsRequired();

		builder.Property(x => x.PostId)
			.HasMaxLength(256)
			.HasConversion(postId => postId.Value, id => new PostId(id))
			;

		builder.HasOne(x => x.Post)
			.WithMany(x => x.Comments)
			.HasForeignKey(x => x.PostId)
			.IsRequired();

		builder.HasOne(x => x.User)
			.WithMany(x => x.Comments)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.NoAction)
			.IsRequired();
	}
}
