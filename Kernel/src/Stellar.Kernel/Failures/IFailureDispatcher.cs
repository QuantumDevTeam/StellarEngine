using Stellar.Kernel.Quantization;
using Stellar.Kernel.Data.Context;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Failure main dispatcher for handling exceptions
    /// </summary>
    public interface IFailureDispatcher
        : IQuant
    {
        /// <summary>
        /// Dispatch failure to transfer him in a handler
        /// </summary>
        /// <param name="failureContext">Context of Failure</param>
        /// <returns>Is stop execution</returns>
        bool Dispatch(IContext<IFailureContextData> failureContext);
    }
}