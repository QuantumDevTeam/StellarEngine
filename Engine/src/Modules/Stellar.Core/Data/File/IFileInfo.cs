namespace Stellar.Core.Data.File;

/// <summary>
/// Provides metadata information about a file.
/// </summary>
public interface IFileInfo
{
    /// <summary>
    /// File name (last segment of the path).
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Full path within the domain (as stored in Location.Path).
    /// </summary>
    string FullPath { get; }

    /// <summary>
    /// Size in bytes. May be -1 if unknown.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// Creation time (UTC). May be null if not supported.
    /// </summary>
    DateTime? CreationTimeUtc { get; }

    /// <summary>
    /// Last write time (UTC). May be null if not supported.
    /// </summary>
    DateTime? LastWriteTimeUtc { get; }

    /// <summary>
    /// True if the entry is a directory (for file systems that support directories).
    /// </summary>
    bool IsDirectory { get; }

    /// <summary>
    /// True if the file exists.
    /// </summary>
    bool Exists { get; }
}