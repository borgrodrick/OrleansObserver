using Orleans;
using OrleansObserver.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrleansObserver.Grains
{
    public class SourceGrain : Grain, ISourceGrain
    {
        private ObserverSubscriptionManager<IObserve> subscribers;


        public override Task OnActivateAsync()
        {
            subscribers = new ObserverSubscriptionManager<IObserve>();

            // set up timer to simulate events to subscribe to
            RegisterTimer(SendOutUpdates, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

            return TaskDone.Done;
        }


        public Task SubscribeForUpdates(IObserve subscriber)
        {
            subscribers.Subscribe(subscriber);
            return TaskDone.Done;
        }

        private Task SendOutUpdates(object _)
        {
            subscribers.Notify(s => s.StuffUpdate(DateTime.Now.Millisecond));

            return TaskDone.Done;
        }
    }
}