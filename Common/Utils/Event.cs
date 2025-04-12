namespace ConcursMotociclism.Utils;

public record Event(EventType eventType, object data)
{
    public EventType eventType { get; init; } = eventType;
    public object Data { get; init; } = data;
}