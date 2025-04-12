namespace ConcursMotociclism.Communication;

public class Response(ResponseType responseType, string responseJson)
{
    public ResponseType ResponseType { get; } = responseType;
    public string ResponseJson { get; } = responseJson;

    public override string ToString()
    {
        return $"Response{{responseType={ResponseType}, responseJson={ResponseJson}}}";
    }
}