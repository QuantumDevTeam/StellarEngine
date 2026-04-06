using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// Quantum File type
    /// </summary>
    public interface IFileType
        : IRegistrableMetaQuant, ILabeled, IEquatable<IFileType>
    {
        /// <summary>
        /// FileType string
        /// </summary>
        /// <returns>string equals with FileType</returns>
        string ToString();
    }
}