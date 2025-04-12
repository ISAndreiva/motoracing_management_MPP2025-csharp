using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RaceRegistrationDto(Guid id, RaceDto raceDto, RacerDto racerDto)
{
    public Guid Id { get; set; } = id;
    public RaceDto RaceDto { get; set; } = raceDto;
    public RacerDto RacerDto { get; set; } = racerDto;

    public static RaceRegistrationDto FromRaceRegistration(RaceRegistration raceRegistration)
    {
        return new RaceRegistrationDto(raceRegistration.Id, RaceDto.fromRace(raceRegistration.Race), RacerDto.fromRacer(raceRegistration.Racer));
    }

    public RaceRegistration ToRaceRegistration()
    {
        return new RaceRegistration(Id, RaceDto.ToRace(), RacerDto.ToRacer());
    }
}