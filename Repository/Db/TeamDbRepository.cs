using System.Data;
using ConcursMotociclism.domain;
using log4net;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class TeamDbRepository() : AbstractDbRepository<Team, Guid>("team"), ITeamRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
    
    
    public override Team Get(Guid id)
    {
        return base.Get(id, "uuid");
    }
    
    public override void Add(Team entity)
    {
        logger.Info("Adding team to database");
        try
        {
            const string sql = "INSERT INTO team (uuid, name) VALUES (@uuid, @name)";
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

            statement.ExecuteNonQuery();
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }

    public override void Update(Team entity)
    {
        logger.Info("Updating team with id:" + entity.Id);
        try
        {
            const string sql = "UPDATE team SET name = @name WHERE uuid = @uuid";
            using var connection = DbUtils.GetConnection();
            using var statement = connection.CreateCommand();
            statement.CommandText = sql;

            var paramName = statement.CreateParameter();
            paramName.ParameterName = "name";
            paramName.Value = entity.Name;
            statement.Parameters.Add(paramName);
            var paramGuid = statement.CreateParameter();
            paramGuid.ParameterName = "uuid";
            paramGuid.Value = entity.Id.ToString();
            statement.Parameters.Add(paramGuid);

            statement.ExecuteNonQuery();
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }

    protected override Team ExtractEntity(IDataReader reader)
    {
        return new Team(reader.GetGuid(0), reader.GetString(1));
    }
}