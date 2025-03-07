using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IRepositoryInterface<TE> where TE : Entity
{
    void Add(TE entity);

    TE Get(Guid id);

    IEnumerable<TE> GetAll();

    void Update(TE entity);

    void Remove(Guid id);
}