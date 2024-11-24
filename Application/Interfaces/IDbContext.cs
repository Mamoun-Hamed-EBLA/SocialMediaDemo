namespace Application.Interfaces;

public interface IDbContext
{
	DbSet<PostEntity> Posts { get; }
	DbSet<UserEntity> DomainUsers { get; }
	DbSet<CommentEntity> Comments { get; }
	DbSet<PostUserLikeEntity> Likes { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}