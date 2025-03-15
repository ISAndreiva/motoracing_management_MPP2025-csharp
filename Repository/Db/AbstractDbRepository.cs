using System.Data;
using ConcursMotociclism.domain;
using Microsoft.Data.Sqlite;

namespace ConcursMotociclism.Repository.Db;

public abstract class AbstractDbRepository<TE, TId>(string tableName) : IRepository<TE, TId>
    where TE : Entity<TId>
{
    protected DbUtils DbUtils { get; set; } = new DbUtils();
    protected string TableName = tableName;

    public abstract void Add(TE entity);
    
    public abstract void Update(TE entity);
    
    protected abstract TE ExtractEntity(IDataReader reader);
    
    protected IEnumerable<TE> GetEntitiesByField(string fieldName, object fieldValue)
    {
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
            Program.log.Error($"Error executing query: {ex.Message}");
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

    public TE Get(TId id)
    {
        try
        {
            var connection = DbUtils.GetConnection();
            var queryCommand = connection.CreateCommand();
            queryCommand.CommandText = $"SELECT * FROM {TableName} WHERE uuid = @id";

            var paramGuid = queryCommand.CreateParameter();
            paramGuid.ParameterName = "@id";
            paramGuid.Value = id.ToString();
            queryCommand.Parameters.Add(paramGuid);


            var reader = queryCommand.ExecuteReader();
            reader.Read();
            return ExtractEntity(reader);
        } catch(Exception ex)
        {
            Program.log.Error($"Error executing query: {ex.Message}");
            return null;
        }
    }

    public IEnumerable<TE> GetAll()
    {
        IDataReader reader = null;
        try
        {
            var connection = DbUtils.GetConnection();
            var queryCommand = connection.CreateCommand();
            queryCommand.CommandText = $"SELECT * FROM {TableName}";
            reader = queryCommand.ExecuteReader();
        } catch (Exception ex)
        {
            Program.log.Error($"Error executing query: {ex.Message}");
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
            Program.log.Error($"Error executing query: {ex.Message}");
        }
    }
}