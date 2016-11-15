using Orleans;
using System.Threading.Tasks;

namespace OrleansObserver.Interfaces
{
    public interface IObserverGrain : IObserve, IGrainWithIntegerKey
    {
        Task Start();
    }
}