using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using OrleansObserver.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrelansObservier.Client
{
    class Program
    {
        static int Main(string[] args)
        {

            var config = ClientConfiguration.LocalhostSilo();
            try
            {
                InitializeWithRetries(config, initializeAttemptsBeforeFailing: 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Orleans client initialization failed failed due to {ex}");

                Console.ReadLine();
                return 1;
            }

            DoClientWork().Wait();
            Console.WriteLine("Press Enter to terminate...");
            Console.ReadLine();
            return 0;
        }

        private static void InitializeWithRetries(ClientConfiguration config, int initializeAttemptsBeforeFailing)
        {
            int attempt = 0;
            while (true)
            {
                try
                {
                    GrainClient.Initialize(config);
                    Console.WriteLine("Client successfully connect to silo host");
                    break;
                }
                catch (SiloUnavailableException)
                {
                    attempt++;
                    Console.WriteLine($"Attempt {attempt} of {initializeAttemptsBeforeFailing} failed to initialize the Orleans client.");
                    if (attempt > initializeAttemptsBeforeFailing)
                    {
                        throw;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            }
        }

        private static async Task DoClientWork()
        {
            IObserverGrain observerGrain = GrainClient.GrainFactory.GetGrain<IObserverGrain>(0);
            await observerGrain.Start();

            var theObserver = new TheObserver();
            var obj = GrainClient.GrainFactory.CreateObjectReference<IObserve>(theObserver).Result; // factory from IObserve

            ISourceGrain sourceGrain = GrainClient.GrainFactory.GetGrain<ISourceGrain>(0);
            await sourceGrain.SubscribeForUpdates(obj);
            //await sourceGrain.SubscribeForUpdates(observerGrain);

            Console.WriteLine("Client is running.\nPress Enter to terminate...");
            Console.ReadLine();
        }

        // class for handling updates from grain
        private class TheObserver : IObserve
        {
            // Receive updates 
            public void StuffUpdate(int data)
            {
                Console.WriteLine("New stuff from Class: {0}", data);
            }
        }
    }
}
