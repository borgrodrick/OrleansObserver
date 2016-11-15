using Orleans;
using OrleansObserver.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrleansObserver.Grains
{
    public class ObserverGrain : Grain, IObserverGrain
    {
        public void StuffUpdate(int data)
        {
            Console.WriteLine("New stuff from Grain: {0}", data);
        }

        public Task Start()
        {
            ISourceGrain sourceGrain = GrainFactory.GetGrain<ISourceGrain>(0);

            sourceGrain.SubscribeForUpdates(this);

            Console.WriteLine("Started ObserverGrain");
            return TaskDone.Done;
        }
    }
}