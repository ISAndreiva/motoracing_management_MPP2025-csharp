using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class TeamDto(Team team)
{
    private readonly Guid _id = team.Id;
    private readonly string _name = team.Name;

    public Team ToTeam()
    {
        return new Team(_id, _name);
    }
    
}