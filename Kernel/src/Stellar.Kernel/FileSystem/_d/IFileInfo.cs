using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Provides metadata information about a file.
    /// </summary>
    public interface IFileInfo : IQuantumObject
    {
        /// <summary>
        /// File name (last segment of the path).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Full path within the domain (as stored in Location.Path).
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Size in bytes. May be -1 if unknown.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Creation time (UTC). May be null if not supported.
        /// </summary>
        DateTime? CreationTimeUtc { get; }

        /// <summary>
        /// Last write time (UTC). May be null if not supported.
        /// </summary>
        DateTime? LastWriteTimeUtc { get; }
    }
}