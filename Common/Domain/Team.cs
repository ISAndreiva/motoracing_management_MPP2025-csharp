namespace ConcursMotociclism.domain;

public class Team(Guid id, string name) : Entity<Guid>(id)
{
    public string Name { get; } = name;

    protected bool Equals(Team other)
    {
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Team)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}