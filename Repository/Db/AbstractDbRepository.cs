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
        var connection = DbUtils.GetConnection();
        var queryCommand = connection.CreateCommand();
        queryCommand.CommandText = $"SELECT * FROM {TableName} WHERE {fieldName} = @value";
        var paramValue = queryCommand.CreateParameter();
        paramValue.ParameterName = "@value";
        paramValue.Value = fieldValue;
        queryCommand.Parameters.Add(paramValue);
        var reader = queryCommand.ExecuteReader();
        while (reader.Read())
        {
            yield return ExtractEntity(reader);
        }
    }

    public TE Get(TId id)
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
    }

    public IEnumerable<TE> GetAll()
    {
        var connection = DbUtils.GetConnection();
        var queryCommand = connection.CreateCommand();
        queryCommand.CommandText = $"SELECT * FROM {TableName}";
        var reader = queryCommand.ExecuteReader();
        while (reader.Read())
        {
            yield return ExtractEntity(reader);
        }
    }
    

    public void Remove(TId id)
    {
        var connection = DbUtils.GetConnection();
        var queryDelete = connection.CreateCommand();
        queryDelete.CommandText = $"DELETE FROM {TableName} WHERE uuid = @id";
        var paramGuid = queryDelete.CreateParameter();
        paramGuid.ParameterName = "@id";
        paramGuid.Value = id.ToString();
        queryDelete.Parameters.Add(paramGuid);
        queryDelete.ExecuteNonQuery();
    }
}