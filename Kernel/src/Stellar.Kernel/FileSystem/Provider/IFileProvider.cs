using System.IO;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem.Provider
{
    /// <summary>
    /// Provides low‑level file operations for a specific domain type.
    /// </summary>
    /// <remarks>
    /// <para>A file provider is a quant (<see cref="IQuant"/>) that implements actual file I/O
    /// for a particular domain (e.g., physical directory, assembly resource, network share).</para>
    /// <para>Providers are obtained from an <see cref="IFileProviderFactory"/> based on the domain.</para>
    /// </remarks>
    public interface IFileProvider
        : IQuant
    {
        /// <summary>
        /// Determines whether this provider can handle files in the specified domain.
        /// </summary>
        /// <param name="domain">The domain to test.</param>
        /// <returns><c>true</c> if this provider supports the domain; otherwise, <c>false</c>.</returns>
        bool CanHandle(IDomain domain);

        /// <summary>
        /// Checks whether a file exists at the given location.
        /// </summary>
        /// <param name="location">The file location to check.</param>
        /// <returns><c>true</c> if the file exists; otherwise, <c>false</c>.</returns>
        bool Exists(ILocation location);

        /// <summary>
        /// Retrieves metadata information about the file at the specified location.
        /// </summary>
        /// <param name="location">The file location.</param>
        /// <returns>An <see cref="IFileInfo"/> instance containing file metadata.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
        IFileInfo GetFileInfo(ILocation location);

        /// <summary>
        /// Opens the file for read‑only access.
        /// </summary>
        /// <param name="location">The file location.</param>
        /// <returns>A readable <see cref="Stream"/>.</returns>
        Stream OpenRead(ILocation location);

        /// <summary>
        /// Opens the file for write‑only access.
        /// </summary>
        /// <param name="location">The file location.</param>
        /// <returns>A writable <see cref="Stream"/>.</returns>
        Stream OpenWrite(ILocation location);

        /// <summary>
        /// Opens the file for both reading and writing.
        /// </summary>
        /// <param name="location">The file location.</param>
        /// <returns>A readable and writable <see cref="Stream"/>.</returns>
        Stream OpenReadWrite(ILocation location);

        /// <summary>
        /// Opens the file with the specified access mode.
        /// </summary>
        /// <param name="location">The file location.</param>
        /// <param name="access">The desired <see cref="FileAccess"/> (Read, Write, or ReadWrite).</param>
        /// <returns>A <see cref="Stream"/> with the requested access.</returns>
        Stream Open(ILocation location, FileAccess access);
    }
}