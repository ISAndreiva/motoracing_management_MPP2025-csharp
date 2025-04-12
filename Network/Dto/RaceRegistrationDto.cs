using ConcursMotociclism.domain;

namespace ConcursMotociclism.dto;

public class RaceRegistrationDto(RaceRegistration raceRegistration)
{
    private readonly Guid _id = raceRegistration.Id;
    private readonly RaceDto _raceDto = new RaceDto(raceRegistration.Race);
    private readonly RacerDto _racerDto = new RacerDto(raceRegistration.Racer);

    public RaceRegistration ToRaceRegistration()
    {
        return new RaceRegistration(_id, _raceDto.ToRace(), _racerDto.ToRacer());
    }
}