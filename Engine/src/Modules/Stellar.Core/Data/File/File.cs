using Stellar.Kernel;
using Stellar.Core.Quantization;

namespace Stellar.Core.Data.File;

public abstract class File(Location location, FileType type, IIdentifier? identifier = null)
    : MetaQuant(identifier),
        IEquatable<File>
{
    public readonly Location Location = location;
    public readonly FileType Type = type;

    public bool Equals(File? other)
    {
        return Type.UID == other?.Type.UID && Location.UID == other.Location.UID;
    }
}