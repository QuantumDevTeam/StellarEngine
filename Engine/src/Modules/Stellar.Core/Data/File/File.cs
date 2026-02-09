using Stellar.Kernel;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.File;

public abstract class File(
    string typeName,
    Path path,
    IIdentifier? identifier = null
) : MetaQuant(identifier)
{
    public string TypeName { get; } = typeName;
    public Path Path { get; } = path;
}