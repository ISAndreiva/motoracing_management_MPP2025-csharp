using System.Data;
using ConcursMotociclism.domain;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public class RaceRegistrationDbRepository() : AbstractDbRepository<RaceRegistration, Guid>("raceregistration"), IRaceRegistrationRepository
{
    public override void Add(RaceRegistration entity)
    {
        try
        {
            const string sql =
                "INSERT INTO raceregistration (uuid, race, racer, class) VALUES (@uuid, @race, @racer, @class)";
            using var connection = DbUtils.GetConnection();
            using var statement = connection.CreateCommand();
            statement.CommandText = sql;

            var paramGuid = statement.CreateParameter();
            paramGuid.ParameterName = "uuid";
            paramGuid.Value = entity.Id.ToString();
            statement.Parameters.Add(paramGuid);
            var paramRace = statement.CreateParameter();
            paramRace.ParameterName = "race";
            paramRace.Value = entity.Race;
            statement.Parameters.Add(paramRace);
            var paramRacer = statement.CreateParameter();
            paramRacer.ParameterName = "racer";
            paramRacer.Value = entity.Racer;
            statement.Parameters.Add(paramRacer);
            var paramClass = statement.CreateParameter();
            paramClass.ParameterName = "class";
            paramClass.Value = entity.RaceClass;
            statement.Parameters.Add(paramClass);

            statement.ExecuteNonQuery();
        } catch (Exception ex)
        {
            Program.log.Error($"Error executing query: {ex.Message}");
        }
    }

    public override void Update(RaceRegistration entity)
    {
        try
        {
            const string sql =
                "UPDATE raceregistration SET race = @race, racer = @racer, class = @class WHERE uuid = @uuid";
            using var connection = DbUtils.GetConnection();
            using var statement = connection.CreateCommand();
            statement.CommandText = sql;

            var paramRace = statement.CreateParameter();
            paramRace.ParameterName = "race";
            paramRace.Value = entity.Race;
            statement.Parameters.Add(paramRace);
            var paramRacer = statement.CreateParameter();
            paramRacer.ParameterName = "racer";
            paramRacer.Value = entity.Racer;
            statement.Parameters.Add(paramRacer);
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
            Program.log.Error($"Error executing query: {ex.Message}");
        }
    }

    protected override RaceRegistration ExtractEntity(IDataReader reader)
    {
        var race = new RaceDbRepository().Get(reader.GetGuid(1));
        var racer = new RacerDbRepository().Get(reader.GetGuid(2));
        return new RaceRegistration(reader.GetGuid(0), race, racer, reader.GetInt32(3));
    }

    public IEnumerable<RaceRegistration> GetRegistrationsByRace(Guid raceId)
    {
        return GetEntitiesByField("race", raceId.ToString());
    }

    public IEnumerable<RaceRegistration> GetRegistrationsByRacer(Guid racerId)
    {
        return GetEntitiesByField("racer", racerId.ToString());
    }
}