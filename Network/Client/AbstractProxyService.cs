using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text.Json;
using ConcursMotociclism.Communication;

namespace ConcursMotociclism.Client;

public abstract class AbstractProxyService(string host, int port)
{
    protected static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("ClientLogger");
    private readonly string _host = host;
    private readonly int _port = port;
    private TcpClient? _clientSocket = null;
    private volatile bool _connected = false;
    private StreamReader? _reader;
    private StreamWriter? _writer;
    protected readonly BlockingCollection<string>? Queue = new BlockingCollection<string>();
    
    protected void InitializeConnection()
    {
        Logger.Info("Connecting to server {host}:{port}");
        try
        {
            _clientSocket = new TcpClient(_host, _port);
            _reader = new StreamReader(_clientSocket.GetStream());
            _writer = new StreamWriter(_clientSocket.GetStream()) { AutoFlush = true };
            _connected = true;
            Logger.Info($"Connected to server {_host}:{_port}");
            StartReader();
        }
        catch (Exception e)
        {
            Logger.Error($"Error connecting to server: {e.Message}");
            throw;
        }
    }
    
    protected void CloseConnection()
    {
        Logger.Info("Closing connection to server");
        try
        {
            _writer?.Close();
            _reader?.Close();
            _clientSocket?.Close();
            _connected = false;
            Logger.Info("Connection closed");
        }
        catch (Exception e)
        {
            Logger.Error($"Error closing connection: {e.Message}");
        }
    }
    
    protected void TestConnection()
    {
        if (_connected && _clientSocket?.Client is { Connected: true }) return;
        Logger.Error("Not connected to server");
        throw new InvalidOperationException("Not connected to server");
    }
    
    protected abstract void HandleUpdate(string response);

    protected ResponseType GetResponseType(string response)
    {
        var type = JsonDocument.Parse(response).RootElement.GetProperty("ResponseType").GetInt32();
        if (!Enum.IsDefined(typeof(ResponseType), type))
        {
            Logger.Error($"Invalid response type: {response}");
            throw new InvalidOperationException($"Invalid response type: {response}");
        }
        return (ResponseType)type;
    }
    
    public void Run()
    {
        Logger.Info("Starting reader thread");
        while (_connected)
        {
            try
            {
                var response = _reader?.ReadLine();
                Logger.Info($"Received response: {response}");
                var responseType = GetResponseType(response);
                if (responseType == ResponseType.Update)
                {
                    HandleUpdate(response);
                }
                else
                {
                    Queue?.Add(response);
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Error reading response: {e.Message}");
                break;
            }
        }
        Logger.Info("Reader thread stopped");
    }
    
    public void StartReader()
    {
        Logger.Info("Starting reader thread");
        var thread = new Thread(Run);
        thread.Start();
    }
    
    public void SendRequest(string request)
    {
        Logger.Info($"Sending request: {request}");
        try
        {
            _writer?.WriteLine(request);
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }
    }
}
