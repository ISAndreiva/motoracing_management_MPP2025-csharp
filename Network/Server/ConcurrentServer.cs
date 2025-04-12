using System.Net.Sockets;
using ConcursMotociclism.Service;

namespace ConcursMotociclism.server;

public class ConcurrentServer(IObservableService service, int port = 9898, string host = "127.0.0.1") : AbstractServer(port, host)
{
    private IObservableService _service = service;
    
    protected override void HandleClient(TcpClient clientSocket)
    {
        var worker = new ClientWorker(_service, clientSocket);
        var thread = new Thread(worker.Run);
        thread.Start();
    }
}