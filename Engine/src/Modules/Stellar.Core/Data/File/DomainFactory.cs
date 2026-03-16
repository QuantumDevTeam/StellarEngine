using Stellar.Core.Data.File.Systems;
using Stellar.Kernel;

namespace Stellar.Core.Data.File;

/// <summary>
/// Domain factory for automatically creating domain with specific File System  
/// </summary>
public static class DomainFactory
{
    private static readonly Dictionary<DomainType, Func<IFileSystem>> _factories = new();

    static DomainFactory()
    {
        Register(DomainType.Directory, () => new DirectoryFileSystem());
        Register(DomainType.Assembly, () => new AssemblyFileSystem());
        Register(DomainType.Http, () => new HttpFileSystem(DomainType.Http));
        Register(DomainType.Https, () => new HttpFileSystem(DomainType.Https));
    }

    public static void Register(DomainType type, Func<IFileSystem> factory)
    {
        _factories[type] = factory;
    }

    public static Domain CreateDomain(DomainType type, string value, IIdentifier? identifier = null)
    {
        if (!_factories.TryGetValue(type, out var factory))
            throw new NotSupportedException($"No file system factory registered for domain type {type}");
        var fileSystem = factory();
        return new Domain(type, value, fileSystem, identifier);
    }
}