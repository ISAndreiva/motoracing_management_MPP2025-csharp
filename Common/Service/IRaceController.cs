using ConcursMotociclism.domain;

namespace ConcursMotociclism.Service;

public interface IRaceController
{
    IEnumerable<int> GetUsedRaceClasses();
    IEnumerable<Race> GetRacesByClass(int raceClass);
    IEnumerable<Race> GetAllRaces();
    Race GetRaceByName(string name);
    Race GetRaceById(Guid id);
    void AddRace(Race race);
    void UpdateRace(Race race);
    void DeleteRace(Guid id);
}