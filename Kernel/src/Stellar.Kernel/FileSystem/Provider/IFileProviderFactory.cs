using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem.Provider
{
    public interface IFileProviderFactory
        : IQuant
    {
        IFileProvider GetFileProvider(IDomain domain);
    }
}