namespace Domain.Interfaces;

public interface IAudibleEntity
{
	DateTime Created { get; }

	DateTime? LastModified { get; }

	void SetCreated(DateTime created);
	void SetLastModified(DateTime lastModified);
}