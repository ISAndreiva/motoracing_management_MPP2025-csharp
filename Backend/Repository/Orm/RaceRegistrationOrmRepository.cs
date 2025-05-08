using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Orm;

public class RaceRegistrationOrmRepository(ConcursMotociclismContext context) : IRaceRegistrationRepository
{
    public void Add(RaceRegistration entity)
    {
        context.RaceRegistrations.Add(entity);
        context.SaveChanges();
    }

    public RaceRegistration Get(Guid id)
    {
        return context.RaceRegistrations.Find(id);
    }

    public IEnumerable<RaceRegistration> GetAll()
    {
        return context.RaceRegistrations.Include(rr => rr.Race).Include(rr => rr.Racer);
    }

    public void Update(RaceRegistration entity)
    {
        context.RaceRegistrations.Update(entity);
        context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        context.RaceRegistrations.Remove(Get(id));
        context.SaveChanges();
    }

    public IEnumerable<RaceRegistration> GetRegistrationsByRace(Guid raceId)
    {
        return context.RaceRegistrations.Where(rr => rr.Race.Id == raceId);
    }

    public IEnumerable<RaceRegistration> GetRegistrationsByRacer(Guid racerId)
    {
        return context.RaceRegistrations.Where(rr => rr.Racer.Id == racerId);
    }
}