using System.Net;
using System.Net.Sockets;

namespace ConcursMotociclism.server;

public abstract class AbstractServer(int port, string host)
{
    protected static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("ServerLogger");
    private readonly int _port = port;
    private readonly string _host = host;
    private TcpListener? _serverSocket = null;
    private volatile bool _running = false;

    public void Start()
    {
        Logger.Info($"Starting server at {_host}:{_port}");
        try
        {
            _serverSocket = new TcpListener(IPAddress.Parse(_host), _port);
            _serverSocket.Start();
            Logger.Info($"Server started at {_host}:{_port}");
            _running = true;
            while (_running)
            {
                Logger.Info("Waiting for clients...");
                var clientSocket = _serverSocket.AcceptTcpClient();
                Logger.Info("Client connected");
                HandleClient(clientSocket);
            }
        }
        catch (Exception e)
        {
            Logger.Error($"Error starting server: {e.Message}");
        }
        finally
        {
            _serverSocket?.Stop();
            Logger.Info("Server stopped");
        }
    }
    
    protected abstract void HandleClient(TcpClient clientSocket);

    public void Stop()
    {
        _running = false;
    }
}