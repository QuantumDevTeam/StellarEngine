using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// An abstract Domain for file location
    /// </summary>
    public interface IDomain
        : IMetaQuant, ILabeled, IEquatable<IDomain>
    {
        /// <summary>
        /// Domain type
        /// </summary>
        DomainType Type { get; }

        /// <summary>
        /// Value of Domain himself
        /// </summary>
        string Value { get; }

        string ToString();
    }
}