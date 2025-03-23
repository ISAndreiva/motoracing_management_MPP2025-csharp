using System.Data;
using ConcursMotociclism.domain;
using log4net;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class RacerDbRepository(ITeamRepository teamRepository) : AbstractDbRepository<Racer, Guid>("racer"), IRacerRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
    
    
    public override Racer Get(Guid id)
    {
        return base.Get(id, "uuid");
    }
    
    public override void Add(Racer entity)
    {
        logger.Info("Adding Racer to database");
        try
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
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }
    
    

    public override void Update(Racer entity)
    {
        logger.Info("Updating Racer with id:" + entity.Id);
        try
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
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }

    public IEnumerable<Racer> GetRacersByTeam(Guid teamId)
    {
        return GetEntitiesByField("team", teamId.ToString());
    }

    public Racer GetRacerByCnp(string cnp)
    {
        using var enumerator = GetEntitiesByField("cnp", cnp).GetEnumerator();
        return (enumerator.MoveNext() ? enumerator.Current : null)!;
    }

    protected override Racer ExtractEntity(IDataReader reader)
    {
        var team = teamRepository.Get(reader.GetGuid(3));
        return new Racer(reader.GetGuid(0), reader.GetString(1), team, reader.GetString(2));
    }
}