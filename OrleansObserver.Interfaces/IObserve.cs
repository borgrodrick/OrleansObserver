using Orleans;

namespace OrleansObserver.Interfaces
{
    public interface IObserve : IGrainObserver
    {
        void StuffUpdate(int data);
    }
}