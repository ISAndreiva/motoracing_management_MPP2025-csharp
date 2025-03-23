using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace ConcursMotociclism.Service;

public class RacerController(IRacerRepository racerRepository)
{
    public IEnumerable<Racer>  GetRacersByTeam(Guid teamId)
    {
        return racerRepository.GetRacersByTeam(teamId);
    }
    
    public Racer GetRacerByCnp(string cnp)
    {
        return racerRepository.GetRacerByCnp(cnp);
    }
    
    public void AddRacer(Racer racer)
    {
        racerRepository.Add(racer);
    }
}