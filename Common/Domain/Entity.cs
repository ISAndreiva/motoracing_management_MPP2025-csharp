namespace ConcursMotociclism.domain;

public class Entity<TId>(TId id)
{
    public TId Id { get; } = id;
}