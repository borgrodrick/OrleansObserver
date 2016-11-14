using Orleans;
using System.Threading.Tasks;

namespace OrleansObserver.Interfaces
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
	public interface IGrain1 : IGrainWithIntegerKey
    {
        Task DoTask();
    }
}
