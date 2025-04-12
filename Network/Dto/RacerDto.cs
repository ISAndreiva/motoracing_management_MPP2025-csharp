using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RacerDto(Guid id, string name, string cnp, TeamDto team)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Cnp { get; set; } = cnp;
    public TeamDto Team { get; set; } = team;

    public static RacerDto fromRacer(Racer racer)
    {
        return new RacerDto(racer.Id, racer.Name, racer.Cnp, TeamDto.FromTeam(racer.Team));
    }
    
    public Racer ToRacer()
    {
        return new Racer(Id, Name, Team.ToTeam(), Cnp);
    }
}