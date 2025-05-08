namespace ConcursMotociclism.domain;

public class Racer : Entity<Guid>
{
    public Racer()
    {
    }
    
    public Racer(Guid id, string name, Team team, string cnp) : base(id)
    {
        Name = name;
        Team = team;
        Cnp = cnp;
    }

    public string Name { get; set; }
    public Team Team { get; set; }
    public string Cnp { get; set; }

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