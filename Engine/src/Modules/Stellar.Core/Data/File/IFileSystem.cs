namespace Stellar.Core.Data.File;

public interface IFileSystem
{
    string Name { get; }
    bool Exists(Location location);
    Stream OpenRead(Location location);
    Stream OpenWrite(Location location);
}