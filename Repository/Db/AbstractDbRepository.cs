using System.Data;
using ConcursMotociclism.domain;
using log4net;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public abstract class AbstractDbRepository<TE, TId>(string tableName) : IRepository<TE, TId>
    where TE : Entity<TId>
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
    protected DbUtils DbUtils { get; set; } = new DbUtils();
    protected string TableName = tableName;

    public abstract void Add(TE entity);
    
    public abstract void Update(TE entity);
    
    protected abstract TE ExtractEntity(IDataReader reader);
    
    protected IEnumerable<TE> GetEntitiesByField(string fieldName, object fieldValue)
    {
        logger.Info("Getting entities by field: " + fieldName + " with value:" + fieldValue);
        IDataReader reader = null;
        try
        {
            var connection = DbUtils.GetConnection();
            var queryCommand = connection.CreateCommand();
            queryCommand.CommandText = $"SELECT * FROM {TableName} WHERE {fieldName} = @value";
            var paramValue = queryCommand.CreateParameter();
            paramValue.ParameterName = "@value";
            paramValue.Value = fieldValue;
            queryCommand.Parameters.Add(paramValue);
            reader = queryCommand.ExecuteReader();
        }
        catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
        if (reader == null)
        {
            yield break;
        }
        while (reader.Read())
        {
            yield return ExtractEntity(reader);
        }
    }

    public abstract TE Get(TId id);

    protected TE Get(TId id, string fieldName)
    {
        logger.Info("Getting entity by id: " + id);
        try
        {
            var connection = DbUtils.GetConnection(); 
            var queryCommand = connection.CreateCommand();
            queryCommand.CommandText = $"SELECT * FROM {TableName} WHERE {fieldName} = @id";

            var paramGuid = queryCommand.CreateParameter();
            paramGuid.ParameterName = "@id";
            paramGuid.Value = id.ToString();
            queryCommand.Parameters.Add(paramGuid);
            
            var reader = queryCommand.ExecuteReader();
            reader.Read();
            return ExtractEntity(reader);
        } catch(Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
            return null;
        }
    }

    public IEnumerable<TE> GetAll()
    {
        logger.Info("Getting all entities from " + tableName);
        IDataReader reader = null;
        try
        {
            var connection = DbUtils.GetConnection();
            var queryCommand = connection.CreateCommand();
            queryCommand.CommandText = $"SELECT * FROM {TableName}";
            reader = queryCommand.ExecuteReader();
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
        if (reader == null)
        {
            yield break;
        }
        while (reader.Read())
        {
            yield return ExtractEntity(reader);
        }
    }
    

    public void Remove(TId id)
    {
        logger.Info("Removing entity with id: " + id);
        try
        {
            var connection = DbUtils.GetConnection();
            var queryDelete = connection.CreateCommand();
            queryDelete.CommandText = $"DELETE FROM {TableName} WHERE uuid = @id";
            var paramGuid = queryDelete.CreateParameter();
            paramGuid.ParameterName = "@id";
            paramGuid.Value = id.ToString();
            queryDelete.Parameters.Add(paramGuid);
            queryDelete.ExecuteNonQuery();
        } catch (Exception ex)
        {
            logger.Error($"Error executing query: {ex.Message}");
        }
    }
}