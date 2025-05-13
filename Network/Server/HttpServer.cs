using System.Net;
using ConcursMotociclism.gRpc;
using ConcursMotociclism.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConcursMotociclism.server;

public class HttpServer(IObservableService service, IRaceController raceController,  int grpcPort, int restPort, string host)
{
    private readonly IObservableService _service = service;
    private readonly IRaceController _raceController = raceController;
    private readonly int _grpcPort = grpcPort;
    private readonly int _restPort = restPort;
    private readonly string _host = host;
    
    public void run()
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .ConfigureServices(services =>
                    {
                        services.AddGrpc();
                        services.AddSingleton<IObservableService>(_service);
                        services.AddControllers();
                        services.AddSingleton<IRaceController>(_raceController);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGrpcService<ServiceRpc>();
                            endpoints.MapControllers();
                        });
                    }).ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Parse(_host), _grpcPort, listenOptions =>
                        {
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                        });
                        options.Listen(IPAddress.Parse(_host), _restPort, listenOptions =>
                        {
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
                        });
                    });
            }).Build().Run();
    }
}