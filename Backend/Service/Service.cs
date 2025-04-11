using System.Collections;
using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;
using ConcursMotociclism.Utils;
using log4net;

namespace ConcursMotociclism.Service;

public class Service(IUserRepository userRepository, ITeamRepository teamRepository, IRaceRepository raceRepository, IRacerRepository racerRepository, IRaceRegistrationRepository raceRegistrationRepository) : IObservableService
{
    private readonly UserController _userController = new UserController(userRepository);
    private readonly TeamController _teamController = new TeamController(teamRepository);
    private readonly RaceController _raceController = new RaceController(raceRepository);
    private readonly RacerController _racerController = new RacerController(racerRepository);
    private readonly RaceRegistrationController _raceRegistrationController = new RaceRegistrationController(raceRegistrationRepository);
    private static readonly ILog Logger = LogManager.GetLogger("Backend.Service");
    private readonly List<IObserver> _observers = [];
    
    public bool CheckUserPassword(string username, string password)
    {
        Logger.Info($"Checking password for user: {username}");
        return _userController.CheckPassword(username, password);
    }

    public IEnumerable<Race> GetRacesByClass(int raceClass)
    {
        Logger.Info($"Getting races for class: {raceClass}");
        return _raceController.GetRacesByClass(raceClass);
    }

    public IEnumerable<int> GetUsedRaceClasses()
    {
        Logger.Info("Getting all used race classes");
        return _raceController.GetUsedRaceClasses();
    }

    public int GetRacersCountForRace(Guid raceId)
    {
        Logger.Info($"Getting number of racers registered for race {raceId}");
        return _raceRegistrationController.GetNumberOfRacersRegisteredForRace(raceId);
    }
    
    public bool CheckUserExists(string username)
    {
        Logger.Info($"Checking if user exists: {username}");
        return _userController.CheckUserExists(username);
    }
    
    public IEnumerable<Racer> GetRacersByTeam(Guid teamId)
    {
        Logger.Info($"Getting racers for team: {teamId}");
        return _racerController.GetRacersByTeam(teamId);
    }
    
    public ISet<int> GetRacerClasses(Guid racerId)
    {
        Logger.Info($"Getting racer classes for id {racerId}");
        return _raceRegistrationController.GetRacerClasses(racerId);
    }

    public IEnumerable<Team> GetTeamsByPartialName(string name)
    {
        Logger.Info($"Getting teams by partial name: {name}");
        return _teamController.GetTeamsByPartialName(name);
    }
    
    public IEnumerable<Team> GetAllTeams()
    {
        Logger.Info("Getting all teams");
        return _teamController.GetAllTeams();
    }
    
    public void AddRacer(Racer racer)
    {
        Logger.Info($"Adding racer {racer.Name} with CNP {racer.Cnp}");
        _racerController.AddRacer(racer);
    }

    public IEnumerable<Race> GetAllRaces()
    {
        Logger.Info("Getting all races");
        return _raceController.GetAllRaces();
    }

    public void AddRaceRegistration(string racerName, string racerCnp, string teamName, string raceName)
    {
        Logger.Info($"Adding race registration for {racerName} in race {raceName}");
        var racer = _racerController.GetRacerByCnp(racerCnp);
        if (racer == null)
        {
            racer = new Racer(Guid.NewGuid(), racerName, _teamController.GetTeamByName(teamName), racerCnp);
            AddRacer(racer);
        }
        var race = _raceController.GetRaceByName(raceName);
        _raceRegistrationController.AddRegistration(new RaceRegistration(Guid.NewGuid(), race, racer));
        NotifyObservers();
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        _observers.ForEach(o => o.update());
    }
}