using System.Data;
using Microsoft.Data.Sqlite;
using System.Configuration;
using log4net;

namespace ConcursMotociclism.Repository;


public class DbUtils()
{
    private static IDbConnection _instance = null;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
    
    private IDbConnection GetNewConnection()
    {
        Logger.Info("Creating new connection");
        var urlLin = ConfigurationManager.ConnectionStrings["linux_db"].ConnectionString;
        var urlWin = ConfigurationManager.ConnectionStrings["windows_db"].ConnectionString;

        var url = Environment.OSVersion.ToString().Contains("Unix") ? urlLin : urlWin;
        Logger.Info("Using connection string: " + url);
        var connection = new SqliteConnection(url);
        return connection;
    }
    
    public IDbConnection GetConnection()
    {
        try
        {
            if (_instance == null || _instance.State == System.Data.ConnectionState.Closed)
            {
                _instance = GetNewConnection();
                _instance.Open();
            }
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return _instance;
    }
}