using System;

namespace Stellar.Kernel.Identification
{
    public interface IIdentifier
    {
        Guid UID { get; }
    }
}