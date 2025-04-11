using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface ITeamRepository : IRepository<Team, Guid>
{
    IEnumerable<Team> getTeamsByPartialName(string  partialName); 
    
    Team getTeamByName(string teamName);
}