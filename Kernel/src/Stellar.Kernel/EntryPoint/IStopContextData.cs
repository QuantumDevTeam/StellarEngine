using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Context data passed to <see cref="StellarEntryPoint.RequestStop"/>.
    /// </summary>
    /// <remarks>
    /// Contains the reason for stopping and optional custom data.
    /// </remarks>
    public interface IStopContext
        : IContext
    {
        /// <summary>
        /// Gets the reason why the stop was requested.
        /// </summary>
        StopReason Reason { get; }
    }
}