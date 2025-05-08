using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Orm;

public class RacerOrmRepository(ConcursMotociclismContext context) : IRacerRepository
{
    public void Add(Racer entity)
    {
        context.Racers.Add(entity);
        context.SaveChanges();
    }

    public Racer Get(Guid id)
    {
        return context.Racers.Find(id);
    }

    public IEnumerable<Racer> GetAll()
    {
        return context.Racers.Include(r => r.Team);
    }

    public void Update(Racer entity)
    {
        context.Racers.Update(entity);
        context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        context.Racers.Remove(Get(id));
        context.SaveChanges();
    }

    public IEnumerable<Racer> GetRacersByTeam(Guid teamId)
    {
        return context.Racers.Where(r => r.Team.Id == teamId);
    }

    public Racer GetRacerByCnp(string cnp)
    {
        return context.Racers.FirstOrDefault(r => r.Cnp == cnp);
    }
}