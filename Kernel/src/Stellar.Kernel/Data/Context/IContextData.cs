using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    /// <summary>
    /// Marker interface for data passed inside an <see cref="IContext"/>.
    /// </summary>
    /// <remarks>
    /// <para>The interface itself adds no methods, but it allows constraining the type parameter
    /// in generic contexts (like <see cref="IContext.GetData{T}"/>) to types intended for context data.</para>
    /// <para>Implementations must contain concrete fields and properties required for a specific operation
    /// (e.g., <see cref="EntryPoint.IModuleRunContextData"/> or <see cref="EntryPoint.IStopContextData"/>).</para>
    /// </remarks>
    public interface IContextData
        : IQuantumObject
    {
    }
}