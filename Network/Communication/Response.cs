namespace ConcursMotociclism.Communication;

public class Response<T>(ResponseType responseType, T responseData)
{
    public ResponseType ResponseType { get; set; } = responseType;
    public T ResponseData { get; set; } = responseData;

    public override string ToString()
    {
        return $"Response{{responseType={ResponseType}, responseData={ResponseData}}}";
    }
}