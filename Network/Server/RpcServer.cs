using System.Net;
using ConcursMotociclism.gRpc;
using ConcursMotociclism.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConcursMotociclism.server;

public class RpcServer(IObservableService service,  int port, string host)
{
    private readonly IObservableService _service = service;
    private readonly int _port = port;
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
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGrpcService<ServiceRpc>();

                        });
                    }).ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Parse(_host), _port, listenOptions =>
                        {
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                        });
                    });
            }).Build().Run();
    }
}