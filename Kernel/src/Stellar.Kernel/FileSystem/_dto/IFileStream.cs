using System;
using System.IO;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Provides a quantum‑aware wrapper around a raw file stream.
    /// </summary>
    /// <remarks>
    /// <para>An <see cref="IFileStream"/> combines a quantum <see cref="IFile"/> reference
    /// with a standard .NET <see cref="Stream"/> for reading/writing content.</para>
    /// <para>It is a meta‑quant, disposable, and equatable. The underlying stream should be disposed
    /// when the quantum stream is disposed.</para>
    /// </remarks>
    public interface IFileStream
        : IMetaQuant, IDisposable, IEquatable<IFileStream>
    {
        /// <summary>
        /// Gets the quantum file associated with this stream.
        /// </summary>
        IFile File { get; }

        /// <summary>
        /// Gets the underlying .NET stream for direct I/O operations.
        /// </summary>
        Stream Stream { get; }
    }
}