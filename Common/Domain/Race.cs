namespace ConcursMotociclism.domain;

public class Race : Entity<Guid>
{
    public Race()
    {
    }

    public Race(Guid id, string raceName, int raceClass) : base(id)
    {
        RaceName = raceName;
        RaceClass = raceClass;
    }

    public string RaceName { get; set; }
    public int RaceClass { get; set; }

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