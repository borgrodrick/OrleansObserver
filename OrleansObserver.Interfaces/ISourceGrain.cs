using Orleans;
using System.Threading.Tasks;

namespace OrleansObserver.Interfaces
{
    public interface ISourceGrain : IGrainWithIntegerKey
    {
        Task SubscribeForUpdates(IObserve subscriber);
    }
}