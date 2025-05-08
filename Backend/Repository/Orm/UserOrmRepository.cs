using ConcursMotociclism.domain;
using ConcursMotociclism.Repository;

namespace Backend.Repository.Orm;

public class UserOrmRepository(ConcursMotociclismContext context) : IUserRepository
{
    public void Add(User entity)
    {
        context.Users.Add(entity);
        context.SaveChanges();
    }

    public User Get(Guid id)
    {
        return context.Users.Find(id);
    }

    public IEnumerable<User> GetAll()
    {
        return context.Users;
    }

    public void Update(User entity)
    {
        context.Users.Update(entity);
        context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        context.Users.Remove(Get(id));
    }

    public User GetUserByUsername(string username)
    {
        return context.Users.FirstOrDefault(u => u.Username == username);
    }
}