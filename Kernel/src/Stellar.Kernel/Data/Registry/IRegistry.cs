using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Registry
{
    public interface IRegistry<T>
        : IQuantumObject
        where T : IRegistrableQuantumObject
    {
        bool Exists(IIdentifier id);
        bool Register(T obj);
#if NETSTANDARD2_0
        T Get(IIdentifier id);
        T Pop(IIdentifier id);
#else
#nullable enable
        T? Get(IIdentifier id);
        T? Pop(IIdentifier id);
#endif
        int Size { get; }

        System.Collections.Generic.ICollection<IIdentifier> Keys { get; }
        System.Collections.Generic.ICollection<T> Values { get; }
    }
}