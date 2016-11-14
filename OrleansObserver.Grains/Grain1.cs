using Orleans;
using OrleansObserver.Interfaces;
using System.Threading.Tasks;

namespace OrleansObserver.Grains
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class Grain1 : Grain, IGrain1
    {
        public Task DoTask()
        {
            return TaskDone.Done;
        }
    }
}
