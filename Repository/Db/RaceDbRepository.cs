using System.Data;
using ConcursMotociclism.domain;
using log4net;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class RaceDbRepository() : AbstractDbRepository<Race, Guid>("race"), IRaceRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
    public override void Add(Race entity)
    {
        logger.Info("Adding new Race to database");
        try
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
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }

    public override Race Get(Guid id)
    {
        return base.Get(id, "uuid");
    }

    public override void Update(Race entity)
    {
        logger.Info("Updating Race with id:" + entity.Id);
        try
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
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }

    protected override Race ExtractEntity(IDataReader reader)
    {
        return new Race(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2));
    }

    public IEnumerable<Race> GetRacesByClass(int raceClass)
    {   
        logger.Info("Getting Races by class " + raceClass);
        return GetEntitiesByField("class", raceClass);
    }

    public IEnumerable<int> GetUsedRaceClasses()
    {
        logger.Info("Getting all used race classes");
        IDataReader reader = null;
        try
        {
            var sql = "SELECT DISTINCT class from race";
            using var connection = DbUtils.GetConnection();
            using var statement = connection.CreateCommand();
            statement.CommandText = sql;
            reader = statement.ExecuteReader();
        }
        catch (Exception e)
        {
            logger.Error($"Error executing query: {e.Message}");
        }
        if (reader == null)
        {
            yield break;
        }
        while (reader.Read())
        {
            yield return reader.GetInt32(0);
        }
    }

    public Race GetRaceByName(string raceName)
    {
        using var enumerator = GetEntitiesByField("name", raceName).GetEnumerator();
        return enumerator.MoveNext() ? enumerator.Current : null;
    }
}