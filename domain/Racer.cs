namespace ConcursMotociclism.domain;

public class Racer(Guid id, string name, Guid teamId, string cnp) : Entity(id)
{
    public string Name { get; } = name;
    public Guid TeamId { get; set; } = teamId;
    public string Cnp { get; } = cnp;

    protected bool Equals(Racer other)
    {
        return Name == other.Name && TeamId.Equals(other.TeamId) && Cnp == other.Cnp;
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
        return HashCode.Combine(Name, TeamId, Cnp);
    }
}