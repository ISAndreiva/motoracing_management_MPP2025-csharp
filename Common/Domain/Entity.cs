namespace ConcursMotociclism.domain;

public class Entity<TId>
{
    public Entity()
    {
    }
    
    public Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; set; }
}