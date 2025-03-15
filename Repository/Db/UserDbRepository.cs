using System.Data;
using ConcursMotociclism.domain;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class UserDbRepository() : AbstractDbRepository<User, Guid>("user")
{
    public override void Add(User entity)
    {
        const string sql = "INSERT INTO user (uuid, username, password_hash) VALUES (@uuid, @username, @password)";
        using var connection = DbUtils.GetConnection();
        using var statement = connection.CreateCommand();
        statement.CommandText = sql;
        
        var paramGuid = statement.CreateParameter();
        paramGuid.ParameterName = "uuid";
        paramGuid.Value = entity.Id.ToString();
        statement.Parameters.Add(paramGuid);
        var paramUsername = statement.CreateParameter();
        paramUsername.ParameterName = "username";
        paramUsername.Value = entity.Username;
        statement.Parameters.Add(paramUsername);
        var paramPassword = statement.CreateParameter();
        paramPassword.ParameterName = "password";
        paramPassword.Value = entity.PasswordHash;
        statement.Parameters.Add(paramPassword);

        statement.ExecuteNonQuery();
    }

    public override void Update(User entity)
    {
        const string sql = "UPDATE user SET username = @username, password_hash = @password WHERE uuid = @uuid";
        using var connection = DbUtils.GetConnection();
        using var statement = connection.CreateCommand();
        statement.CommandText = sql;
        
        var paramUsername = statement.CreateParameter();
        paramUsername.ParameterName = "username";
        paramUsername.Value = entity.Username;
        statement.Parameters.Add(paramUsername);
        var paramPassword = statement.CreateParameter();
        paramPassword.ParameterName = "password";
        paramPassword.Value = entity.PasswordHash;
        statement.Parameters.Add(paramPassword);
        var paramGuid = statement.CreateParameter();
        paramGuid.ParameterName = "uuid";
        paramGuid.Value = entity.Id.ToString();
        statement.Parameters.Add(paramGuid);

        statement.ExecuteNonQuery();
    }

    protected override User ExtractEntity(IDataReader reader)
    {
        return new User(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
    }
}