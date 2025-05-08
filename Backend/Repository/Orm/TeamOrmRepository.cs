using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace Backend.Repository.Orm;

public class TeamOrmRepository(ConcursMotociclismContext context) : ITeamRepository
{
    public void Add(Team entity)
    {
        context.Teams.Add(entity);
        context.SaveChanges();
    }

    public Team Get(Guid id)
    {
        return context.Teams.Find(id);
    }

    public IEnumerable<Team> GetAll()
    {
        return context.Teams;
    }

    public void Update(Team entity)
    {
        context.Teams.Update(entity);
        context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        context.Teams.Remove(Get(id));
        context.SaveChanges();
    }

    public IEnumerable<Team> getTeamsByPartialName(string partialName)
    {
        return context.Teams.Where(t => t.Name.Contains(partialName));
    }

    public Team getTeamByName(string teamName)
    {
        return context.Teams.FirstOrDefault(t => t.Name == teamName);
    }
}