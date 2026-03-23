using Stellar.Kernel.Quantization;

namespace Stellar.Core.Quantization;

public interface IRegistrableQuantInterface<T, TMeta> : IQuant, IDisposable
    where T : IRegistrableQuantInterface<T, TMeta>
    where TMeta : IMetaQuant
{
}