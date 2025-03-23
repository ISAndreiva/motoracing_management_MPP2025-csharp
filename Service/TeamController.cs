using System.Collections;
using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace ConcursMotociclism.Service;

public class TeamController(ITeamRepository teamRepository)
{
    public IEnumerable<Team> GetTeamsByPartialName(string partialName)
    {
        return teamRepository.getTeamsByPartialName(partialName);
    }
    
    public Team GetTeamByName(string teamName)
    {
        return teamRepository.getTeamByName(teamName);
    }

    public IEnumerable<Team> GetAllTeams()
    {
        return teamRepository.GetAll();
    }
}