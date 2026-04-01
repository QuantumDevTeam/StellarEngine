using System;
using Stellar.Kernel.Label;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem
{
    public interface IDomain : IMetaQuant, ILabeled, IEquatable<IDomain>
    {
        /// <summary>
        /// Domain type
        /// </summary>
        DomainType Type { get; }

        /// <summary>
        /// Domain himself
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Domain string
        /// </summary>
        /// <returns>string equals with domain</returns>
        string ToString();
    }
}