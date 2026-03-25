using System;
using System.IO;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Abstract Quantum File stream for operating with file content
    /// </summary>
    public interface IFileStream : IMetaQuant, IDisposable, IEquatable<IFileStream>
    {
        /// <summary>
        /// Quantum File
        /// </summary>
        IFile File { get; }
        
        /// <summary>
        /// File stream
        /// </summary>
        Stream Stream { get; }
    }
}