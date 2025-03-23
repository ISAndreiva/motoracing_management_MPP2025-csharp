namespace ConcursMotociclism.domain;

public class RaceRegistration(Guid id, Race race, Racer racer, int raceClass) : Entity<Guid>(id)
{
    public Race Race { get; } = race;
    public Racer Racer { get; } = racer;

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