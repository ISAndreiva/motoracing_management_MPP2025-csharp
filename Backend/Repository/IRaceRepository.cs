using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IRaceRepository : IRepository<Race, Guid>
{
    IEnumerable<Race> GetRacesByClass(int raceClass);
    
    IEnumerable<int> GetUsedRaceClasses();
    
    Race GetRaceByName(string raceName);
}