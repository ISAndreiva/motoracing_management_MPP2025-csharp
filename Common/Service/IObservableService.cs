using ConcursMotociclism.Utils;

namespace ConcursMotociclism.Service;

public interface IObservableService : IService
{
    public void RegisterObserver(IObserver observer);
    
    public void UnregisterObserver(IObserver observer);
    
    protected void NotifyObservers(EventType type, object data);
}