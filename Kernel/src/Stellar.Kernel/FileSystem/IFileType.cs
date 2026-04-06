using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    public interface IFileType
        : IRegistrableMetaQuant, ILabeled, IEquatable<IFileType>
    {
        string ToString();
    }
}