using ConcursMotociclism.Repository;

namespace ConcursMotociclism.Service;

public class UserController(IUserRepository userRepository)
{
    public bool CheckPassword(string username, string passwordHash)
    {
        var hash = userRepository.GetUserByUsername(username).PasswordHash;
        return hash.Equals(passwordHash);
    }
    
    public bool CheckUserExists(string username)
    {
        return userRepository.GetUserByUsername(username) != null;
    }
}