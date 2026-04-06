using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    /// <summary>
    /// An abstract Quantum Location in his Domain
    /// </summary>
    public interface ILocation
        : IMetaQuant, IEquatable<ILocation>
    {
        /// <summary>
        /// File domain
        /// </summary>
        IDomain Domain { get; }

        /// <summary>
        /// File path in domain
        /// </summary>
        string Path { get; }
        
        /// <summary>
        /// Location string
        /// </summary>
        /// <returns>string equals with location</returns>
        string ToString();
    }
}