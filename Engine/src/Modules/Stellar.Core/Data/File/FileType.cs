using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.Core.Data.File;

public class FileType(string name, IFileSystem fileSystem, IIdentifier? identifier = null)
    : RegistrableMetaQuant<FileType>(identifier)
{
    public readonly string Name = name;
    public readonly IFileSystem FileSystem = fileSystem;

    public override string ToString() => $"{FileSystem.Name}:{Name}";
}