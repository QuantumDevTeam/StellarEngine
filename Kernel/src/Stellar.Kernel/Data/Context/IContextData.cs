using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    /// <summary>
    /// Marker interface for data passed inside an <see cref="IContext"/>.
    /// </summary>
    /// <remarks>
    /// <para>The interface itself adds no methods, but it allows constraining the type parameter
    /// in generic contexts to types intended for context data.</para>
    /// <para>Implementations must contain concrete fields and properties required for a specific operation</para>
    /// <para>Use IContextData only on structures!!!</para>
    /// </remarks>
    public interface IContextData
        : IQuantumObject
    {
    }
}