using Domain.Entities;
using Domain.StrongTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;
public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
{
	public void Configure(EntityTypeBuilder<PostEntity> builder)
	{
		builder.HasKey(x => x.Id)
			.IsClustered(false);

		builder.Property(x => x.Id)
			.HasMaxLength(450)
			.HasConversion(postId => postId.Value, id => new PostId(id))
			;

		builder.Property(x => x.Post)
			.HasMaxLength(512)
			.IsRequired();


	}
}
