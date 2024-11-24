using Domain.Interfaces;

namespace Domain.Base;
public abstract class Entity<TKey> : IEntity<TKey>
{
	public TKey Id { get; init; }

	public DateTime Created { get; private set; }

	public DateTime? LastModified { get; private set; }

	public void SetCreated(DateTime created) { Created = created; }
	public void SetLastModified(DateTime lastModified) { LastModified = lastModified; }
}
