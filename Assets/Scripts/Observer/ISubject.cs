namespace Observer
{
    public interface ISubject
    {
        void RemoveObserver(IObserver observer);
        void AddObserver(IObserver observer);
        void NotifyObservers();
    }
}