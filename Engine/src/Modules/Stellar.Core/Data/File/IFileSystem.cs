namespace Stellar.Core.Data.File;

/// <summary>
/// An abstract FileSystem used for operating with FileTypes associated with this FileSystem
/// </summary>
public interface IFileSystem
{
    /// <summary>
    /// FileSystem name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Check Quantum File to existing
    /// </summary>
    /// <param name="location">Quantum File Location</param>
    /// <returns>Quantum File is exist?</returns>
    bool Exists(Location location);

    /// <summary>
    /// Open Quantum File for reading
    /// </summary>
    /// <param name="location">Quantum File Location</param>
    /// <returns>Data Stream for this location</returns>
    Stream OpenRead(Location location);

    /// <summary>
    /// Open Quantum File for writing
    /// </summary>
    /// <param name="location">Quantum File Location</param>
    /// <returns>Data Stream for this location</returns>
    Stream OpenWrite(Location location);

    /// <summary>
    /// Retrieves metadata information about the file at the specified location
    /// </summary>
    /// <param name="location">File location</param>
    /// <returns>File meta-data</returns>
    IFileInfo GetInfo(Location location);
}