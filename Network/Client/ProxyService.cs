using System.Text.Json;
using ConcursMotociclism.Communication;
using ConcursMotociclism.domain;
using ConcursMotociclism.dto;
using ConcursMotociclism.Service;
using ConcursMotociclism.Utils;

namespace ConcursMotociclism.Client;

public class ProxyService(string host = "127.0.0.1", int port = 9898) : AbstractProxyService(host, port), IObservableService
{
    private readonly List<IObserver> _observers = [];
    private Dictionary<Race, int> _raceRacersNoCache = new Dictionary<Race, int>();
    private volatile bool _cacheValid = false;
    
    protected override void HandleUpdate(string response)
    {
        Logger.Info("Received update: " + response);
        var responseObject = JsonSerializer.Deserialize<Response<Event>>(response);
        var eventObject = responseObject?.ResponseData;
        if (eventObject is { type: EventType.RaceRegistration })
        {
            _cacheValid = false;
            var raceDto = JsonSerializer.Deserialize<RaceDto>(eventObject.data.ToString());
            NotifyObservers(eventObject.type, raceDto.ToRace());
        }
    }

    public bool CheckUserPassword(string username, string password)
    {
        try
        {
            TestConnection();
        }
        catch (InvalidOperationException e)
        {
            InitializeConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        var userCredentials = new UserCredentialsDto(username, password);
        SendRequest(JsonSerializer.Serialize(new Request<UserCredentialsDto>(RequestType.CheckUserPassword, userCredentials)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.Error)
        {
            CloseConnection();
        }
        return responseType == ResponseType.Ok;
    }

    public IEnumerable<Race> GetRacesByClass(int raceClass)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<int>(RequestType.GetRacesByClass, raceClass)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetRacesByClass)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<RaceDto>>>(response);
            foreach (var r in responseObject.ResponseData) 
            {
                yield return r.ToRace();
            }
        }
    }

    public IEnumerable<int> GetUsedRaceClasses()
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<object>(RequestType.GetUsedRaceClasses, null)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetUsedRaceClasses)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<int>>>(response);
            foreach (var r in responseObject.ResponseData) 
            {
                yield return r;
            }
        }
    }

    public int GetRacersCountForRace(Guid raceId)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }

        if (!_cacheValid)
        {
            UpdateCache();
        }

        Race wantedRace = null;
        foreach (var race in _raceRacersNoCache.Keys)
        {
            if (race.Id.Equals(raceId))
                wantedRace = race;
        }
        return wantedRace != null ? _raceRacersNoCache[wantedRace] : 0;
    }

    public bool CheckUserExists(string username)
    {
        try
        {
            TestConnection();
        }
        catch (InvalidOperationException e)
        {
            InitializeConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
        }
        SendRequest(JsonSerializer.Serialize(new Request<string>(RequestType.CheckUserExists, username)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.Error)
        {
            CloseConnection();
        }
        return responseType == ResponseType.Ok;
    }

    public IEnumerable<Racer> GetRacersByTeam(Guid teamId)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<Guid>(RequestType.GetRacersByTeam, teamId)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetRacersByTeam)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<RacerDto>>>(response);
            foreach (var r in responseObject.ResponseData) 
            {
                yield return r.ToRacer();
            }
        }
    }

    public ISet<int> GetRacerClasses(Guid racerId)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<Guid>(RequestType.GetRacerClasses, racerId)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetRacerClasses)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<int>>>(response);
            var result = new HashSet<int>();
            foreach (var r in responseObject.ResponseData) 
            {
                result.Add(r);
            }
            return result;
        }
        return new HashSet<int>();
    }

    public IEnumerable<Team> GetTeamsByPartialName(string name)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<string>(RequestType.GetTeamsByPartialName, name)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetTeamsByPartialName)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<TeamDto>>>(response);
            foreach (var r in responseObject.ResponseData) 
            {
                yield return r.ToTeam();
            }
        }
    }

    public IEnumerable<Team> GetAllTeams()
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<object>(RequestType.GetAllTeams, null)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetAllTeams)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<TeamDto>>>(response);
            foreach (var r in responseObject.ResponseData) 
            {
                yield return r.ToTeam();
            }
        }
    }

    public void AddRacer(Racer racer)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        var racerDto = RacerDto.fromRacer(racer);
        SendRequest(JsonSerializer.Serialize(new Request<RacerDto>(RequestType.AddRacer, racerDto)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.Error)
        {
            Logger.Error("Error adding racer");
            throw new InvalidOperationException("Error adding racer");
        }
    }

    public IEnumerable<Race> GetAllRaces()
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<object>(RequestType.GetAllRaces, null)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetAllRaces)
        {
            var responseObject = JsonSerializer.Deserialize<Response<List<RaceDto>>>(response);
            foreach (var r in responseObject.ResponseData) 
            {
                yield return r.ToRace();
            }
        }
    }

    public void AddRaceRegistration(string racerName, string racerCnp, string teamName, string raceName)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        var racerDto = new RacerDto(Guid.Empty, racerName, racerCnp, new TeamDto(Guid.Empty, teamName));
        var raceDto = new RaceDto(Guid.Empty, raceName, 0);
        var registrationDto = new RaceRegistrationDto(Guid.Empty, raceDto, racerDto);
        SendRequest(JsonSerializer.Serialize(new Request<RaceRegistrationDto>(RequestType.AddRaceRegistration, registrationDto)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.Error)
        {
            Logger.Error("Error adding race registration: " );
            throw new InvalidOperationException("Error adding race registration"); 
        }
    }

    public Race GetRaceByName(string raceName)
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        SendRequest(JsonSerializer.Serialize(new Request<string>(RequestType.GetRaceByName, raceName)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetRaceByName)
        {
            var responseObject = JsonSerializer.Deserialize<Response<RaceDto>>(response);
            return responseObject.ResponseData.ToRace();
        }
        return null;
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        _observers.Remove(observer);
        if (_observers.Count == 0)
        {
            CloseConnection();
        }
    }

    public void NotifyObservers(EventType type, object data)
    {
        foreach (var observer in _observers)
        {
            observer.Update(type, data);
        }
    }

    public void UpdateCache()
    {
        try
        {
            TestConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e.Message);
            throw;
        }
        _raceRacersNoCache.Clear();
        SendRequest(JsonSerializer.Serialize(new Request<object>(RequestType.GetRacesAndRacersNo, null)));
        var response = Queue?.Take();
        var responseType = GetResponseType(response);
        if (responseType == ResponseType.GetRacesAndRacersNo)
        {
            var responseObject = JsonSerializer.Deserialize<Response<Dictionary<string, int>>>(response);
            responseObject.ResponseData.ToList().ForEach(x => _raceRacersNoCache.Add(RaceDto.FromString(x.Key).ToRace(), x.Value));
            _cacheValid = true;
        }
    }
}