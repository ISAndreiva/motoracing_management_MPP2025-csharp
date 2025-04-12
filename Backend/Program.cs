// See https://aka.ms/new-console-template for more information

using ConcursMotociclism.server;
using System.Configuration;
using System.Reflection;
using ConcursMotociclism.Repository.Db;
using ConcursMotociclism.Service;
using log4net;
using log4net.Config;

class Program
{
    static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        
        AbstractServer server = null;
        
        var userDbRepository = new UserDbRepository();
        var teamDbRepository = new TeamDbRepository();
        var raceDbRepository = new RaceDbRepository();
        var racerDbRepository = new RacerDbRepository(teamDbRepository);
        var raceRegistrationDbRepository = new RaceRegistrationDbRepository(raceDbRepository, racerDbRepository);
        var service = new Service(userDbRepository, teamDbRepository, raceDbRepository, racerDbRepository, raceRegistrationDbRepository);
        
        if (ConfigurationManager.AppSettings["port"] == null || ConfigurationManager.AppSettings["host"] == null)
        {
            server = new ConcurrentServer(service);
        }
        else
        {
            var port = ConfigurationManager.AppSettings["port"];
            var host = ConfigurationManager.AppSettings["host"];
            server = new ConcurrentServer(service, int.Parse(port), host);
        }
        server.Start();
        
        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            server.Stop();
            Console.WriteLine("Server stopped");
        };
    }
}