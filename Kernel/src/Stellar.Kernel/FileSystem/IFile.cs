using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// An abstract Quantum File stored in abstract Quantum Location
    /// </summary>
    public interface IFile
        : IMetaQuant, IEquatable<IFile>
    {
        /// <summary>
        /// File location
        /// </summary>
        ILocation Location { get; }

        /// <summary>
        /// File type
        /// </summary>
        IFileType Type { get; }
    }
}