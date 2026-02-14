using Stellar.Core.Quantization;
using Stellar.Kernel;

namespace Stellar.Core.Data.File;

public class FileType(string name, string extension, IIdentifier? identifier = null)
    : RegistrableMetaQuant<FileType>(identifier)
{
    public readonly string Name = name;
    public readonly string Extension = extension;
}