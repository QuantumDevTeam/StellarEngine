using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    public interface IContext<out TData>
        : IQuantumObject
        where TData : IContextData
    {
        IQuantumObject sender { get; }
#if NETSTANDARD2_0
        TData Data { get; }
#else
#nullable enable
        TData? Data { get; }
#endif
    }
}