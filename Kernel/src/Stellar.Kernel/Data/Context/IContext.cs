using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    public interface IContext<out TData>
        : IQuantumObject
        where TData : IContextData
    {
#if NETSTANDARD2_0
        IQuantumObject Sender { get; }
        TData Data { get; }
#else
#nullable enable
        IQuantumObject? Sender { get; }
        TData? Data { get; }
#endif
    }
}