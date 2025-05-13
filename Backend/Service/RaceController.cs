using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace ConcursMotociclism.Service;

public class RaceController(IRaceRepository raceRepository) : IRaceController
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
    
    public Race GetRaceById(Guid id)
    {
        return raceRepository.Get(id);
    }

    public void AddRace(Race race)
    {
        raceRepository.Add(race);
    }

    public void UpdateRace(Race race)
    {
        raceRepository.Update(race);
    }
    
    public void DeleteRace(Guid id)
    {
        raceRepository.Remove(id);
    }
}