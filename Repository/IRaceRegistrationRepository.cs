using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IRaceRegistrationRepository : IRepository<RaceRegistration, Guid>
{
    IEnumerable<RaceRegistration> GetRegistrationsByRace(Guid raceId);
    IEnumerable<RaceRegistration> GetRegistrationsByRacer(Guid racerId);
}