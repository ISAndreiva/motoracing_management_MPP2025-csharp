using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace Backend.Repository.Orm;

public class RaceOrmRepository(ConcursMotociclismContext context) : IRaceRepository
{
    public void Add(Race entity)
    {
        context.Races.Add(entity);
        context.SaveChanges();
    }

    public Race Get(Guid id)
    {
        return context.Races.Find(id);
    }

    public IEnumerable<Race> GetAll()
    {
        return context.Races;
    }

    public void Update(Race entity)
    {
        context.Races.Update(entity);
        context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        context.Races.Remove(Get(id));
        context.SaveChanges();
    }

    public IEnumerable<Race> GetRacesByClass(int raceClass)
    {
        return context.Races.Where(r => r.RaceClass == raceClass);
    }

    public IEnumerable<int> GetUsedRaceClasses()
    {
        return context.Races.Select(r => r.RaceClass).Distinct();
    }

    public Race GetRaceByName(string raceName)
    {
        return context.Races.FirstOrDefault(r => r.RaceName == raceName);
    }
}