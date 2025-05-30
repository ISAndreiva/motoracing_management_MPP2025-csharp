using ConcursMotociclism.domain;

namespace ConcursMotociclism.Repository;

public interface IUserRepository : IRepository<User, Guid>
{
    User GetUserByUsername(string username);
}