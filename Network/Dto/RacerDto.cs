using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RacerDto(Racer racer)
{
    private readonly Guid _id = racer.Id;
    private readonly string _name = racer.Name;
    private readonly string _cnp = racer.Cnp;
    private readonly TeamDto _team = new TeamDto(racer.Team);
    
    public Racer ToRacer()
    {
        return new Racer(_id, _name, _team.ToTeam(), _cnp);
    }
}