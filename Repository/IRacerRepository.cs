using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IRacerRepository : IRepository<Racer, Guid>
{
    IEnumerable<Racer> GetRacersByTeam(Guid teamId);
}