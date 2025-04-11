using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace ConcursMotociclism.Service;

public class RaceRegistrationController(IRaceRegistrationRepository raceRegistrationRepository)
{
    public int GetNumberOfRacersRegisteredForRace(Guid raceId)
    {
        return raceRegistrationRepository.GetRegistrationsByRace(raceId).Count();
    }

    public ISet<int> GetRacerClasses(Guid racerId)
    {
        HashSet<int> racerClasses = [];
        var registrations = raceRegistrationRepository.GetRegistrationsByRacer(racerId);
        foreach (var registration in registrations)
        {
            racerClasses.Add(registration.Race.RaceClass);
        }
        return racerClasses;
    }
    
    public void AddRegistration(RaceRegistration registration)
    {
        raceRegistrationRepository.Add(registration);
    }
}