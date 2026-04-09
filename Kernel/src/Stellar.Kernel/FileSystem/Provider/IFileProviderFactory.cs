using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem.Provider
{
    /// <summary>
    /// Resolves an appropriate file provider for a given domain.
    /// </summary>
    /// <remarks>
    /// <para>The factory is a quant that maintains a mapping from domain types (or domain instances)
    /// to registered <see cref="IFileProvider"/> implementations. It is used by higher‑level file system APIs
    /// to delegate operations to the correct provider.</para>
    /// </remarks>
    public interface IFileProviderFactory
        : IQuant
    {
        /// <summary>
        /// Returns a file provider capable of handling the specified domain.
        /// </summary>
        /// <param name="domain">The domain for which a provider is needed.</param>
        /// <returns>An <see cref="IFileProvider"/> that can operate on the given domain.</returns>
        /// <exception cref="NotSupportedException">Thrown if no provider can handle the domain.</exception>
        IFileProvider GetFileProvider(IDomain domain);
    }
}