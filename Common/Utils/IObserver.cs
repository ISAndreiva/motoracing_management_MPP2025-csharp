namespace ConcursMotociclism.Utils;

public interface IObserver
{
    void Update(EventType type, object data);
}