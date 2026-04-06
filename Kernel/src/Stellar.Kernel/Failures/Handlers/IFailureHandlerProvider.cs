using System.Collections.Generic;
using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures.Handlers
{
    /// <summary>
    /// Handler provider for Handler registered in this Provider
    /// </summary>
    public interface IFailureHandlerProvider
        : IRegistrableQuant
    {
        /// <summary>
        /// Gets all appropriate handlers
        /// </summary>
        /// <param name="failureContext">Context of Failure</param>
        /// <returns>All appropriate handlers</returns>
        IEnumerable<IFailureHandler> GetHandlers(IContext<IFailureContextData> failureContext);
    }
}