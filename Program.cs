using System.Reflection;
using ConcursMotociclism.domain;
using ConcursMotociclism.Repository.Db;
using log4net;
using log4net.Config;

namespace ConcursMotociclism;

public static class Program
{
    public static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        
        
        var userDbRepository = new UserDbRepository();
        var teamDbRepository = new TeamDbRepository();
        var raceDbRepository = new RaceDbRepository();
        var racerDbRepository = new RacerDbRepository(teamDbRepository);
        var raceRegistrationDbRepository = new RaceRegistrationDbRepository(raceDbRepository, racerDbRepository);

        foreach (var VARIABLE in raceRegistrationDbRepository.GetRegistrationsByRace(Guid.Parse("f58e3001-6c97-4f21-9602-91024bb3fccf")))
        {
            Console.WriteLine(VARIABLE.Race.RaceName + " " + VARIABLE.RaceClass);
        }
    }
}