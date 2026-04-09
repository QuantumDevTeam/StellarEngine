using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Threading
{
    /// <summary>
    /// Marker interface for task‑specific data that can be stored in an <see cref="ITaskContext{T}"/>.
    /// </summary>
    /// <remarks>
    /// This interface does not add any members; it simply identifies a class or structure as being valid
    /// task data for the threading subsystem. It inherits from <see cref="IContextData"/> to integrate
    /// with the general data context system.
    /// </remarks>
    public interface ITaskData : IContextData
    {
    }
}