using ConcursMotociclism.domain;
using ConcursMotociclism.Service;
using ConcursMotociclism.Utils;
using Grpc.Core;

namespace ConcursMotociclism.gRpc;

public class ServiceRpc : ProxyService.ProxyServiceBase, IObserver
{
    private readonly IObservableService service;
    
    public ServiceRpc(IObservableService service)
    {
        this.service = service;
        this.service.RegisterObserver(this);
    }

    public override Task<checkUserPasswordResponse> checkUserPassword(checkUserPasswordRequest request, ServerCallContext context)
    {
        var result = service.CheckUserPassword(request.User.Username, request.User.Password);
        return Task.FromResult(new checkUserPasswordResponse
        {
            PasswordGood = result,
        });
    }

    public override Task<getRacesByClassResponse> getRacesByClass(getRacesByClassRequest request, ServerCallContext context)
    {
        var result = service.GetRacesByClass(request.RaceClass);
        var races = result.Select(race => new RaceRpc { Id = race.Id.ToString(), RaceName = race.RaceName, RaceClass = race.RaceClass }).ToList();

        return Task.FromResult(new getRacesByClassResponse
        {
            Races = { races },
        });
    }

    public override Task<getUsedRaceClassesResponse> getUsedRaceClasses(EmptyRequest request, ServerCallContext context)
    {
        var result = service.GetUsedRaceClasses();
        return Task.FromResult(new getUsedRaceClassesResponse
        {
            RaceClasses = { result },
        });
    }

    public override Task<getRacersCountForRaceResponse> getRacersCountForRace(getRacersCountForRaceRequest request, ServerCallContext context)
    {
        var result = service.GetRacersCountForRace(Guid.Parse(request.RaceId));
        return Task.FromResult(new getRacersCountForRaceResponse
        {
            RacersCount = result,
        });
    }

    public override Task<StatusResponse> checkUserExists(checkUserExistsRequest request, ServerCallContext context)
    {
        var result = service.CheckUserExists(request.Username);
        return Task.FromResult(new StatusResponse
        {
            Status = result ? StatusResponse.Types.Status.Ok : StatusResponse.Types.Status.Error,
        });
    }

    public override Task<getRacersByTeamResponse> getRacersByTeam(getRacersByTeamRequest request, ServerCallContext context)
    {
        var result = service.GetRacersByTeam(Guid.Parse(request.TeamId));
        var racers = new List<RacerRpc>();
        foreach (var racer in result)
        {
            var racerRpc = new RacerRpc
            {
                Id = racer.Id.ToString(),
                Name = racer.Name,
                Cnp = racer.Cnp
            };
            var teamRpc = new TeamRpc
            {
                Id = racer.Team.Id.ToString(),
                Name = racer.Team.Name
            };
            racerRpc.Team = teamRpc;
            racers.Add(racerRpc);
        }
        return Task.FromResult(new getRacersByTeamResponse
        {
            Racers = { racers },
        });
    }

    public override Task<getRacerClassesResponse> getRacerClasses(getRacerClassesRequest request, ServerCallContext context)
    {
        var result = service.GetRacerClasses(Guid.Parse(request.RacerId));
        return Task.FromResult(new getRacerClassesResponse
        {
            Classes = { result },
        });
    }

    public override Task<getTeamsByPartialNameResponse> getTeamsByPartialName(getTeamsByPartialNameRequest request, ServerCallContext context)
    {
        var result = service.GetTeamsByPartialName(request.PartialName);
        var teams = result.Select(team => new TeamRpc { Id = team.Id.ToString(), Name = team.Name }).ToList();
        return Task.FromResult(new getTeamsByPartialNameResponse
        {
            Teams = { teams },
        });
    }

    public override Task<getAllTeamsResponse> getAllTeams(EmptyRequest request, ServerCallContext context)
    {
        var result = service.GetAllTeams();
        var teams = result.Select(team => new TeamRpc { Id = team.Id.ToString(), Name = team.Name }).ToList();
        return Task.FromResult(new getAllTeamsResponse
        {
            Teams = { teams },
        });
    }

    public override Task<StatusResponse> addRacer(addRacerRequest request, ServerCallContext context)
    {
        var requestRacer = request.Racer;
        var team = new Team(Guid.Parse(requestRacer.Team.Id), requestRacer.Team.Name);
        var racer = new Racer(Guid.Parse(requestRacer.Id), requestRacer.Name, team, requestRacer.Cnp);
        service.AddRacer(racer);
        return Task.FromResult(new StatusResponse
        {
            Status = StatusResponse.Types.Status.Ok,
        });
    }

    public override Task<getAllRacesResponse> getAllRaces(EmptyRequest request, ServerCallContext context)
    {
        var result = service.GetAllRaces();
        var races = result.Select(race => new RaceRpc { Id = race.Id.ToString(), RaceName = race.RaceName, RaceClass = race.RaceClass }).ToList();
        return Task.FromResult(new getAllRacesResponse
        {
            Races = { races }
        });
    }

    public override Task<StatusResponse> addRaceRegistration(addRaceRegistrationRequest request, ServerCallContext context)
    {
        service.AddRaceRegistration(request.RacerName, request.RacerCNP, request.TeamName, request.RaceName);
        return Task.FromResult(new StatusResponse
        {
            Status = StatusResponse.Types.Status.Ok,
        });
    }

    public override Task<getRaceByNameResponse> getRaceByName(getRaceByNameRequest request, ServerCallContext context)
    {
        var result = service.GetRaceByName(request.RaceName);
        return Task.FromResult(new getRaceByNameResponse
        {
            Race = new RaceRpc
            {
                Id = result.Id.ToString(),
                RaceName = result.RaceName,
                RaceClass = result.RaceClass,
            }
        });
    }

    public override async Task SubscribeToUpdates(EmptyRequest request, IServerStreamWriter<UpdateResponse> responseStream, ServerCallContext context)
    {
        void SendUpdate(EventType eventType, Object data)
        {
            if (eventType == EventType.RaceRegistration)
            {
                var race = (Race)data;
                var raceRpc = new RaceRpc
                {
                    Id = race.Id.ToString(),
                    RaceClass = race.RaceClass,
                    RaceName = race.RaceName,
                };
                responseStream.WriteAsync(new UpdateResponse
                {
                    Event = new EventRpc
                    {
                        Race = raceRpc,
                        Type = EventRpc.Types.Type.RaceRegistration,
                    }
                });
            }
        }

        NotificationCenter.onUpdate += SendUpdate;
        
        try
        {
            await Task.Delay(Timeout.Infinite, context.CancellationToken);
        }
        catch (TaskCanceledException)
        {
        }
        finally
        {
            NotificationCenter.onUpdate -= SendUpdate; // clean up subscription
        }
    }

    public void Update(EventType type, object data)
    {
        NotificationCenter.Update(type, data);
    }
}

public static class NotificationCenter
{
    public static event Action<EventType, Object> onUpdate;

    public static void Update(EventType type, Object data)
    {
        onUpdate?.Invoke(type, data);
    }
}