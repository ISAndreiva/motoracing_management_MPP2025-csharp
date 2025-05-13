using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RaceDto(string id, string raceName, int raceClass)
{
    public string Id { get; set; } = id;
    public string RaceName { get; set; } = raceName;
    public int RaceClass { get; set; } = raceClass;

    public static RaceDto fromRace(Race race)
    {
        return new RaceDto(race.Id.ToString(), race.RaceName, race.RaceClass);
    }
    
    public Race ToRace()
    {
        return new Race(Guid.Parse(Id), RaceName, RaceClass);
    }
    
    public override string ToString()
    {
        return $"id={Id}, raceName={RaceName}, raceClass={RaceClass}";
    }
    
    public static RaceDto FromString(string str)
    {
        var parts = str.Split(',');
        if (parts.Length != 3)
        {
            throw new ArgumentException("Invalid string format for RaceDto");
        }

        var id = parts[0].Split('=')[1].Trim();
        var raceName = parts[1].Split('=')[1].Trim();
        var raceClass = int.Parse(parts[2].Split('=')[1].Trim());

        return new RaceDto(id, raceName, raceClass);
    }
}