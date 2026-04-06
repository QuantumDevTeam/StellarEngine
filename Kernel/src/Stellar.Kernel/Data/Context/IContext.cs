using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    /// <summary>
    /// Execution context
    /// </summary>
    /// <typeparam name="TData">Context Data</typeparam>
    /// <remarks>
    /// used in all operations, as example: EntryPoint.Run/RequestStop or SceneNode.Update/Render
    /// </remarks>
    public interface IContext<out TData>
        : IQuantumObject
        where TData : IContextData
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Quant that caused contextual execution
        /// </summary>
        IQuantumObject Sender { get; }
        
        /// <summary>
        /// Context Data
        /// </summary>
        TData Data { get; }
#else
#nullable enable
        /// <summary>
        /// Quant that caused contextual execution
        /// </summary>
        IQuantumObject? Sender { get; }
        
        /// <summary>
        /// Context Data
        /// </summary>
        TData? Data { get; }
#endif
    }
}