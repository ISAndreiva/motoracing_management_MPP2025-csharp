using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RaceDto(Guid id, string raceName, int raceClass)
{
    public Guid Id { get; set; } = id;
    public string RaceName { get; set; } = raceName;
    public int RaceClass { get; set; } = raceClass;

    public static RaceDto fromRace(Race race)
    {
        return new RaceDto(race.Id, race.RaceName, race.RaceClass);
    }
    
    public Race ToRace()
    {
        return new Race(Id, RaceName, RaceClass);
    }
}