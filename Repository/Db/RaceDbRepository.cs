using System.Data;
using ConcursMotociclism.domain;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class RaceDbRepository() : AbstractDbRepository<Race, Guid>("race"), IRaceRepository
{
    public override void Add(Race entity)
    {
        const string sql = "INSERT INTO race (uuid, name, class) VALUES (@uuid, @name, @class)";
        using var connection = DbUtils.GetConnection();
        using var statement = connection.CreateCommand();
        statement.CommandText = sql;
        
        var paramGuid = statement.CreateParameter();
        paramGuid.ParameterName = "uuid";
        paramGuid.Value = entity.Id.ToString();
        statement.Parameters.Add(paramGuid);
        var paramName = statement.CreateParameter();
        paramName.ParameterName = "name";
        paramName.Value = entity.RaceName;
        statement.Parameters.Add(paramName);
        var paramClass = statement.CreateParameter();
        paramClass.ParameterName = "class";
        paramClass.Value = entity.RaceClass;
        statement.Parameters.Add(paramClass);

        statement.ExecuteNonQuery();
    }

    public override void Update(Race entity)
    {
        const string sql = "UPDATE race SET name = @name, class = @class WHERE uuid = @uuid";
        using var connection = DbUtils.GetConnection();
        using var statement = connection.CreateCommand();
        statement.CommandText = sql;
        
        var paramName = statement.CreateParameter();
        paramName.ParameterName = "name";
        paramName.Value = entity.RaceName;
        statement.Parameters.Add(paramName);
        var paramClass = statement.CreateParameter();
        paramClass.ParameterName = "class";
        paramClass.Value = entity.RaceClass;
        statement.Parameters.Add(paramClass);
        var paramGuid = statement.CreateParameter();
        paramGuid.ParameterName = "uuid";
        paramGuid.Value = entity.Id.ToString();
        statement.Parameters.Add(paramGuid);

        statement.ExecuteNonQuery();
    }

    protected override Race ExtractEntity(IDataReader reader)
    {
        return new Race(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2));
    }

    public IEnumerable<Race> GetRacesByClass(int raceClass)
    {
        return GetEntitiesByField("class", raceClass);
    }
}