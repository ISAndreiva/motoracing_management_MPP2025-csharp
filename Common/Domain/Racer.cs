namespace ConcursMotociclism.domain;

public class Racer(Guid id, string name, Team team, string cnp) : Entity<Guid>(id)
{
    public string Name { get; } = name;
    public Team Team { get; set; } = team;
    public string Cnp { get; } = cnp;

    protected bool Equals(Racer other)
    {
        return Name == other.Name && Team.Equals(other.Team) && Cnp == other.Cnp;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Racer)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Team, Cnp);
    }
}