using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;
using ConcursMotociclism.Communication;
using ConcursMotociclism.domain;
using ConcursMotociclism.dto;
using ConcursMotociclism.Service;
using ConcursMotociclism.Utils;

namespace ConcursMotociclism.server;

public class ClientWorker : IObserver
{
    private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("ClientWorker");
    private readonly IObservableService _service;
    private readonly TcpClient _connection;
    private volatile bool _connected;
    private StreamReader? _reader;
    private StreamWriter? _writer;

    public ClientWorker(IObservableService service, TcpClient clientSocket)
    {
        _service = service;
        service.RegisterObserver(this);
        _connection = clientSocket;
    }
    
    public void Run()
    {
        Logger.Info("Client worker started");
        try
        {
            var stream = _connection.GetStream();
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = true };
            _connected = true;
        }catch (Exception e)
        {
            Logger.Error(e);
        }
        
        while (_connected)
        {
            try
            {
                var request = _reader.ReadLine();
                Logger.Info($"Received request: {request}");
                string response = HandleRequest(request);
                if (string.IsNullOrEmpty(response))
                {
                    Logger.Error("Received empty response");
                }
                Logger.Info($"Sending response: {response}");
                SendResponse(response);
            }
            catch (IOException e)
            {
                Logger.Error($"Connection error: {e.Message}");
                break;
            }
        }
        Logger.Info("Client disconnected");
        try
        {
            _service.UnregisterObserver(this);
            _reader?.Close();
            _writer?.Close();
            _connection.Close();
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }
    }
    
    private void SendResponse(string response)
    {
        try
        {
            _writer?.WriteLine(response);
        }
        catch (IOException e)
        {
            Logger.Error($"Error sending response: {e.Message}");
        }
    }

    private string HandleRequest(string request)
    {
        if (string.IsNullOrEmpty(request))
        {
            Logger.Error("Received empty request");
            return string.Empty;
        }
        var type = JsonDocument.Parse(request).RootElement.GetProperty("RequestType").GetInt32();
        if (!Enum.IsDefined(typeof(RequestType), type))
        {
            Logger.Error($"Invalid request type: {request}");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var requestType = (RequestType)type;
        var function = "Handle" + requestType;
        var response = string.Empty;
        try
        {
            Logger.Info("Invoking method: " + function);
            var method = GetType().GetMethod(function, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            response = (string) method?.Invoke(this, new object[] { request });
        }catch (TargetInvocationException tie)
        {
            Logger.Error($"Error handling request: {tie.InnerException?.Message}");
            Logger.Error($"Stack Trace: {tie.InnerException?.StackTrace}");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }catch (Exception e)
        {
            Logger.Error($"Error handling request: {e.Message}");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        if (response.Length == 0)
        {
            Logger.Info($"Received null response for request: {request}");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        return response;
    }
    
    private string HandleCheckUserPassword(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<UserCredentialsDto>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }

        if (_service.CheckUserPassword(requestObject.RequestData.username, requestObject.RequestData.passwordHash))
        {
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Ok, null));
        }
        return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
    }

    private string HandleGetRacesByClass(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<int>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var racesDto = _service.GetRacesByClass(requestObject.RequestData).Select(RaceDto.fromRace).ToList();
        return JsonSerializer.Serialize(new Response<List<RaceDto>>(ResponseType.GetRacesByClass, racesDto));
    }

    private string HandleGetUsedRaceClasses(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<object>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }

        var raceClasses = _service.GetUsedRaceClasses().ToList();
        return JsonSerializer.Serialize(new Response<List<int>>(ResponseType.GetUsedRaceClasses, raceClasses));
    }

    private string HandleGetRacersCountForRace(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<Guid>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var count = _service.GetRacersCountForRace(requestObject.RequestData);
        return JsonSerializer.Serialize(new Response<int>(ResponseType.GetRacersCountForRace, count));
    }

    private string HandleCheckUserExists(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<string>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }

        if (_service.CheckUserExists(requestObject.RequestData))
        {
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Ok, null));
        }
        return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
    }

    private string HandleGetRacersByTeam(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<Guid>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var racersDto = _service.GetRacersByTeam(requestObject.RequestData).Select(RacerDto.fromRacer).ToList();
        return JsonSerializer.Serialize(new Response<List<RacerDto>>(ResponseType.GetRacersByTeam, racersDto));
    }

    private string HandleGetRacerClasses(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<Guid>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var classes = _service.GetRacerClasses(requestObject.RequestData).ToList();
        return JsonSerializer.Serialize(new Response<List<int>>(ResponseType.GetRacerClasses, classes));
    }

    private string HandleGetTeamsByPartialName(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<string>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var teamsDto = _service.GetTeamsByPartialName(requestObject.RequestData).Select(TeamDto.FromTeam).ToList();
        return JsonSerializer.Serialize(new Response<List<TeamDto>>(ResponseType.GetTeamsByPartialName, teamsDto));
    }

    private string HandleGetAllTeams(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<object>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var teamsDto = _service.GetAllTeams().Select(TeamDto.FromTeam).ToList();
        return JsonSerializer.Serialize(new Response<List<TeamDto>>(ResponseType.GetAllTeams, teamsDto));
    }

    private string HandleAddRacer(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<RacerDto>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }

        try
        {
            _service.AddRacer(requestObject.RequestData.ToRacer());
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Ok, null));
        }
        catch (Exception e)
        {
            Logger.Error(e);
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
    }

    private string HandleGetAllRaces(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<object>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var racesDto = _service.GetAllRaces().Select(RaceDto.fromRace).ToList();
        return JsonSerializer.Serialize(new Response<List<RaceDto>>(ResponseType.GetAllRaces, racesDto));
    }

    private string HandleAddRaceRegistration(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<RaceRegistrationDto>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        try
        {
            var raceRegistration = requestObject.RequestData.ToRaceRegistration();
            _service.AddRaceRegistration(raceRegistration.Racer.Name, raceRegistration.Racer.Cnp, raceRegistration.Racer.Team.Name, raceRegistration.Race.RaceName);
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Ok, null));
        }
        catch (Exception e)
        {
            Logger.Error(e);
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
    }

    private string HandleGetRaceByName(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<string>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }
        var raceDto = RaceDto.fromRace(_service.GetRaceByName(requestObject.RequestData));
        return JsonSerializer.Serialize(new Response<RaceDto>(ResponseType.GetRaceByName, raceDto));
    }

    private string HandleGetRacesAndRacersNo(string request) 
    {
        var requestObject = JsonSerializer.Deserialize<Request<object>>(request);
        if (requestObject == null)
        {
            Logger.Error("Invalid request object");
            return JsonSerializer.Serialize(new Response<object>(ResponseType.Error, null));
        }

        var racesAndRacersNo = _service.GetAllRaces().ToDictionary(
                race => RaceDto.fromRace(race).ToString(),
                race => _service.GetRacersCountForRace(race.Id)
            );
        return JsonSerializer.Serialize(new Response<Dictionary<string, int>>(ResponseType.GetRacesAndRacersNo, racesAndRacersNo));
    }

    public void Update(EventType type, object data)
    {
        Logger.Info("Sending update to client");
        if (type == EventType.RaceRegistration)
            data = RaceDto.fromRace((Race)data);
        SendResponse(JsonSerializer.Serialize(new Response<Event>(ResponseType.Update, new Event(type, data))));
    }
}