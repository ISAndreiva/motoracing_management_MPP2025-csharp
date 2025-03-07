namespace ConcursMotociclism.domain;

public class RaceRegistration(Guid id, Guid raceId, Guid racerId, int raceClass) : Entity(id)
{
    public Guid RaceId { get; } = raceId;
    public Guid RacerId { get; } = racerId;
    public int RaceClass { get; } = raceClass;

    protected bool Equals(RaceRegistration other)
    {
        return RaceId.Equals(other.RaceId) && RacerId.Equals(other.RacerId) && RaceClass == other.RaceClass;
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
        return HashCode.Combine(RaceId, RacerId, RaceClass);
    }
}