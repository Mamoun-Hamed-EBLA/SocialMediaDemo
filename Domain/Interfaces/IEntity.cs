namespace Domain.Interfaces;
public interface IEntity<TKey> : IAudibleEntity
{
	TKey Id { get; }
}
