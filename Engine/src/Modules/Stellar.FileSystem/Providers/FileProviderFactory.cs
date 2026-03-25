using Stellar.Core.Data.Collections;
using Stellar.Kernel.FileSystem;
using Stellar.Kernel.FileSystem.Provider;

namespace Stellar.FileSystem.Providers;

public class FileProviderFactory(DataContainer<IFileProvider> providers) : IFileProviderFactory
{
    public IFileProvider GetFileProvider(IDomain domain)
    {
        var provider = providers.FirstOrDefault(p => p.CanHandle(domain));
        return provider ?? throw new NotSupportedException($"No provider registered for domain `{domain}`.");
    }
}