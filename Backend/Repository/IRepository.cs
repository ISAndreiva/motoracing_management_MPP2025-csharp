using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IRepository<TE, in TId> where TE : Entity<TId>
{
    void Add(TE entity);

    TE Get(TId id);

    IEnumerable<TE> GetAll();

    void Update(TE entity);

    void Remove(TId id);
}