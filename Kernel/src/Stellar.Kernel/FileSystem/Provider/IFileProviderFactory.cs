using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.FileSystem.Provider
{
    public interface IFileProviderFactory : IQuantumObject
    {
        IFileProvider GetFileProvider(IDomain domain);
    }
}