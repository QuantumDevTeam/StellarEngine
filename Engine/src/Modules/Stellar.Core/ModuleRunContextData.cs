using System.Runtime.CompilerServices;
using Stellar.Kernel.Data.Context;
using Stellar.Kernel.EntryPoint;
using Stellar.Kernel.Failures;
using Stellar.Kernel.LoggingSystem;
using Stellar.Kernel.Quantization;

namespace Stellar.Core;

/// <inheritdoc/>
/// <param name="sender">A Sender which create this context</param>
/// <param name="data">A data for context, implements <see cref="IContextData"/></param>
/// <param name="logger">An already initialized logger</param>
/// <param name="failureDispatcher">A failure dispatcher for dispatching failures in module run</param>
public readonly ref struct RunContext(
    IQuantumObject? sender,
    ILogger? logger,
    IFailureDispatcher? failureDispatcher,
    IContextData? data = null
) : IRunContext
{
    /// <inheritdoc/>
    public IQuantumObject? Sender => sender;

    /// <inheritdoc/>
    public IContextData? Data => data;

    /// <inheritdoc/>
    public ILogger? Logger => logger;

    /// <inheritdoc/>
    public IFailureDispatcher? FailureDispatcher => failureDispatcher;

    /// <summary>
    /// Represent typed data
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    /// <returns>Readonly data lives only on stack</returns>
    ref readonly T? GetData<T>()
        where T : struct, IContextData
    {
        if (data == null) return ref Unsafe.NullRef<T?>();
        return ref Unsafe.As<IContextData?, T?>(ref Unsafe.AsRef(in data)!);
    }
}