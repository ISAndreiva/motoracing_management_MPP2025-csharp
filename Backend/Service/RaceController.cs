using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace ConcursMotociclism.Service;

public class RaceController(IRaceRepository raceRepository)
{
    public IEnumerable<int> GetUsedRaceClasses()
    {
        return raceRepository.GetUsedRaceClasses();
    }

    public IEnumerable<Race> GetRacesByClass(int raceClass)
    {
        return raceRepository.GetRacesByClass(raceClass);
    }

    public IEnumerable<Race> GetAllRaces()
    {
        return raceRepository.GetAll();
    }

    public Race GetRaceByName(string name)
    {
        return raceRepository.GetRaceByName(name);
    }
}