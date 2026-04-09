using System;
using Stellar.Kernel.FileSystem.Provider;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Provides metadata information about a file.
    /// </summary>
    /// <remarks>
    /// This interface is returned by <see cref="IFileProvider.GetFileInfo"/> and contains
    /// basic file attributes such as name, size, and timestamps.
    /// </remarks>
    public interface IFileInfo
        : IQuantumObject
    {
        /// <summary>
        /// Gets the file name (the last segment of the path).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full path of the file within its domain (as stored in <see cref="ILocation.Path"/>).
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Gets the file size in bytes. Returns <c>-1</c> if the size is unknown (e.g., for virtual files).
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets the file creation time in UTC, or <c>null</c> if not supported by the underlying provider.
        /// </summary>
        DateTime? CreationTimeUtc { get; }

        /// <summary>
        /// Gets the last write time in UTC, or <c>null</c> if not supported.
        /// </summary>
        DateTime? LastWriteTimeUtc { get; }
    }
}