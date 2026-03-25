using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    public interface IFileType : IMetaQuant, ILabeled, IEquatable<IFileType>
    {
        /// <summary>
        /// FileType string
        /// </summary>
        /// <returns>string equals with FileType</returns>
        string ToString();
    }
}