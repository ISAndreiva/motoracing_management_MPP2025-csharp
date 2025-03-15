using System.Data;
using ConcursMotociclism.domain;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class RacerDbRepository() : AbstractDbRepository<Racer, Guid>("racer"), IRacerRepository
{
    public override void Add(Racer entity)
    {
        const string sql = "INSERT INTO racer (uuid, name, team) VALUES (@uuid, @name, @team)";
        using var connection = DbUtils.GetConnection();
        using var statement = connection.CreateCommand();
        statement.CommandText = sql;
        
        var paramGuid = statement.CreateParameter();
        paramGuid.ParameterName = "uuid";
        paramGuid.Value = entity.Id.ToString();
        statement.Parameters.Add(paramGuid);
        var paramName = statement.CreateParameter();
        paramName.ParameterName = "name";
        paramName.Value = entity.Name;
        statement.Parameters.Add(paramName);
        var paramTeam = statement.CreateParameter();
        paramTeam.ParameterName = "team";
        paramTeam.Value = entity.Team.Id.ToString();
        statement.Parameters.Add(paramTeam);

        statement.ExecuteNonQuery();
    }

    public override void Update(Racer entity)
    {
        const string sql = "UPDATE racer SET name = @name, team = @team WHERE uuid = @uuid";
        using var connection = DbUtils.GetConnection();
        using var statement = connection.CreateCommand();
        statement.CommandText = sql;
        
        var paramName = statement.CreateParameter();
        paramName.ParameterName = "name";
        paramName.Value = entity.Name;
        statement.Parameters.Add(paramName);
        var paramTeam = statement.CreateParameter();
        paramTeam.ParameterName = "team";
        paramTeam.Value = entity.Team.Id.ToString();
        statement.Parameters.Add(paramTeam);
        var paramGuid = statement.CreateParameter();
        paramGuid.ParameterName = "uuid";
        paramGuid.Value = entity.Id.ToString();
        statement.ExecuteNonQuery();
    }

    public IEnumerable<Racer> GetRacersByTeam(Guid teamId)
    {
        return GetEntitiesByField("team", teamId.ToString());
    }

    protected override Racer ExtractEntity(IDataReader reader)
    {
        var team = new TeamDbRepository().Get(reader.GetGuid(3));
        return new Racer(reader.GetGuid(0), reader.GetString(1), team, reader.GetString(2));
    }
}