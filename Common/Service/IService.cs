using ConcursMotociclism.domain;

namespace ConcursMotociclism.Service;

public interface IService
{
    public bool CheckUserPassword(string username, string password);

    public IEnumerable<Race> GetRacesByClass(int raceClass);

    public IEnumerable<int> GetUsedRaceClasses();

    public int GetRacersCountForRace(Guid raceId);

    public bool CheckUserExists(string username);

    public IEnumerable<Racer> GetRacersByTeam(Guid teamId);

    public ISet<int> GetRacerClasses(Guid racerId);

    public IEnumerable<Team> GetTeamsByPartialName(string name);

    public IEnumerable<Team> GetAllTeams();

    public void AddRacer(Racer racer);

    public IEnumerable<Race> GetAllRaces();

    public void AddRaceRegistration(string racerName, string racerCnp, string teamName, string raceName);
    
    public Race GetRaceByName(string raceName);
}