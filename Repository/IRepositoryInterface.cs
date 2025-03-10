using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IRepositoryInterface<TE, TId> where TE : Entity<TId>
{
    void Add(TE entity);

    TE Get(TId id);

    IEnumerable<TE> GetAll();

    void Update(TE entity);

    void Remove(Guid id);
}