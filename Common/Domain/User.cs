namespace ConcursMotociclism.domain;

public class User(Guid id, string username, string passwordHash) : Entity<Guid>(id)
{
    public string Username { get; } = username;
    public string PasswordHash { get; } = passwordHash;

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