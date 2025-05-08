namespace ConcursMotociclism.domain;

public class RaceRegistration : Entity<Guid>
{
    
    public RaceRegistration() { } // EF Core uses this

    public RaceRegistration(Guid id, Race race, Racer racer) : base(id)
    {
        Race = race;
        Racer = racer;
    }
    
    public Race Race { get; set; }
    public Racer Racer { get; set; }
    
    

    protected bool Equals(RaceRegistration other)
    {
        return Race.Equals(other.Race) && Racer.Equals(other.Racer);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((RaceRegistration)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Race, Racer);
    }
}