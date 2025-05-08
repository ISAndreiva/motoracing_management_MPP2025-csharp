namespace ConcursMotociclism.domain;

public class User : Entity<Guid>
{
    public string Username { get; }
    public string PasswordHash { get; }
    
    public User()
    {
    }
    
    public User(Guid id, string username, string passwordHash) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    protected bool Equals(User other)
    {
        return Username == other.Username && PasswordHash == other.PasswordHash;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Username, PasswordHash);
    }
}