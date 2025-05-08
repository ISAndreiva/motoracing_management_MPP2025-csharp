namespace ConcursMotociclism.domain;

public class Team : Entity<Guid>
{
    public Team()
    {
    }
    
    public Team(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public string Name { get; set; }

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