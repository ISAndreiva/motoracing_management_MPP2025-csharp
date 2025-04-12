namespace ConcursMotociclism.Communication;

public class Request<T>(RequestType requestType, T requestData)
{
    public RequestType RequestType { get; set; } = requestType;
    public T RequestData { get; set; } = requestData;

    public override string ToString()
    {
        return $"Request{{requestType={RequestType}, requestData={RequestData}}}";
    }
}