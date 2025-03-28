namespace ConcursMotociclism.Utils;

public class Observable
{
    protected List<Observer> observers = new List<Observer>();
    
    public void addObserver(Observer observer)
    {
        observers.Add(observer);
    }
    
    public void removeObserver(Observer observer)
    {
        observers.Remove(observer);
    }
    
    public void notifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.update();
        }
    }
}