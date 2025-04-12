

using System.Configuration;
using System.Reflection;
using ConcursMotociclism.Client;
using ConcursMotociclism.Gui;
using log4net;
using log4net.Config;

public static class Program
{
    public static void Main(string[] args)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        
        ProxyService service = null;
        if (ConfigurationManager.AppSettings["port"] == null || ConfigurationManager.AppSettings["host"] == null)
        {
            service = new ProxyService();
        }
        else
        {
            var port = ConfigurationManager.AppSettings["port"];
            var host = ConfigurationManager.AppSettings["host"];
            service = new ProxyService(host, int.Parse(port));
        }
        
        ApplicationConfiguration.Initialize();
        var loginView = new LoginView();
        loginView.SetService(service);
        Application.Run(loginView);
    }
}