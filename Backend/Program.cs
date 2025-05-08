using ConcursMotociclism.server;
using System.Configuration;
using System.Reflection;
using ConcursMotociclism.Repository.Db;
using Backend.Repository.Orm;
using ConcursMotociclism.Service;
using log4net;
using log4net.Config;

class Program
{
    static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        
        RpcServer server = null;
        
        // var userDbRepository = new UserDbRepository();
        // var teamDbRepository = new TeamDbRepository();
        // var raceDbRepository = new RaceDbRepository();
        // var racerDbRepository = new RacerDbRepository(teamDbRepository);
        // var raceRegistrationDbRepository = new RaceRegistrationDbRepository(raceDbRepository, racerDbRepository);
        // var service = new Service(userDbRepository, teamDbRepository, raceDbRepository, racerDbRepository, raceRegistrationDbRepository);
        
        var context = new ConcursMotociclismContext();
        var userOrmRepository = new UserOrmRepository(context);
        var teamOrmRepository = new TeamOrmRepository(context);
        var raceOrmRepository = new RaceOrmRepository(context);
        var racerOrmRepository = new RacerOrmRepository(context);
        var raceRegistrationOrmRepository = new RaceRegistrationOrmRepository(context);
        var service = new Service(userOrmRepository, teamOrmRepository, raceOrmRepository, racerOrmRepository, raceRegistrationOrmRepository);

        var port = 9898;
        var host = "localhost";
        if (ConfigurationManager.AppSettings["port"] != null)
        {
            port = int.Parse(ConfigurationManager.AppSettings["port"]);
        }
        if (ConfigurationManager.AppSettings["host"] != null)
        {
            host = ConfigurationManager.AppSettings["host"];
        }
        server = new RpcServer(service, port, host);

        server.run();

    }
}