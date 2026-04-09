using System.Collections.Generic;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures.Handlers
{
    /// <summary>
    /// Provides a collection of appropriate failure handlers for a given failure context.
    /// </summary>
    /// <remarks>
    /// <para>Implementations can filter handlers based on failure type, level, source, or any custom criteria.
    /// The provider itself is a registrable quant, allowing it to be registered in the engine's service container.</para>
    /// </remarks>
    public interface IFailureHandlerProvider
        : IRegistrableQuant
    {
        /// <summary>
        /// Returns all handlers that are suitable for handling the specified failure.
        /// </summary>
        /// <param name="failureContext">The failure context that describes the failure.</param>
        /// <returns>An enumerable collection of <see cref="IFailureHandler"/> instances.</returns>
        /// <remarks>
        /// The order of handlers may be significant – the dispatcher may invoke them in the returned order.
        /// </remarks>
        IEnumerable<IFailureHandler> GetHandlers(IContext<IFailureContextData> failureContext);
    }
}