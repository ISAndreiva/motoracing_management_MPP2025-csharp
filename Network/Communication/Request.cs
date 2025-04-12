namespace ConcursMotociclism.Communication;

public class Request(RequestType requestType, string requestJson)
{
    public RequestType RequestType { get; } = requestType;
    public string RequestJson { get; } = requestJson;

    public override string ToString()
    {
        return $"Request{{requestType={RequestType}, requestJson={RequestJson}}}";
    }
}