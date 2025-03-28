using System.Reflection;
using ConcursMotociclism.domain;
using ConcursMotociclism.Gui;
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
        var service = new Service.Service(userDbRepository, teamDbRepository, raceDbRepository, racerDbRepository, raceRegistrationDbRepository);
        
        ApplicationConfiguration.Initialize();
        var loginView = new LoginView();
        loginView.SetService(service);
        Application.Run(loginView);
    }
}