using System;

namespace Stellar.Kernel
{
    public interface IIdentifier
    {
        Guid UID { get; }
    }
}