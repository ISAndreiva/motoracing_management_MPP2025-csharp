namespace ConcursMotociclism.domain;

public class Race(Guid id, string raceName, int raceClass ) : Entity<Guid>(id)
{
    public string RaceName { get; } = raceName;
    public int RaceClass { get; } = raceClass;

    protected bool Equals(Race other)
    {
        return RaceName == other.RaceName && RaceClass == other.RaceClass;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Race)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(RaceName, RaceClass);
    }
}