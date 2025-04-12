using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class TeamDto(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    public string name { get; set; } = name;

    public static TeamDto FromTeam(Team team)
    {
        return new TeamDto(team.Id, team.Name);
    }

    public Team ToTeam()
    {
        return new Team(Id, name);
    }
    
}