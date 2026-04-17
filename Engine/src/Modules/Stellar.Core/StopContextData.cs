using System.Runtime.CompilerServices;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Quantization;

namespace Stellar.Core;

/// <inheritdoc/>
/// <param name="sender">A sender which create this context</param>
/// <param name="data">A data for context, implements <see cref="IContextData"/></param>
/// <param name="reason">A reason for module stoping</param>
public readonly ref struct StopContext(
    IQuantumObject? sender,
    StopReason reason,
    IContextData? data = null
) : IStopContext
{
    /// <inheritdoc/>
    public IQuantumObject? Sender => sender;

    /// <inheritdoc/>
    public IContextData? Data => data;

    /// <inheritdoc/>
    public StopReason Reason => reason;

    /// <summary>
    /// Represent typed data
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    /// <returns>Readonly data lives only on stack</returns>
    public ref readonly T? GetData<T>()
        where T : struct, IContextData
    {
        if (data == null) return ref Unsafe.NullRef<T?>();
        return ref Unsafe.As<IContextData?, T?>(ref Unsafe.AsRef(in data)!);
    }
}