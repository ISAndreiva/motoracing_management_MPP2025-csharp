using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RaceDto(Race race)
{
    private Guid Id { get;} = race.Id;
    private string RaceName { get; } = race.RaceName;
    private int RaceClass { get; } = race.RaceClass;

    public Race ToRace()
    {
        return new Race(Id, RaceName, RaceClass);
    }
}