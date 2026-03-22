using Stellar.Core.Quantization;

namespace Stellar.FileSystem.File;

/// <summary>
/// Abstract Quantum File stream for operating with file content
/// </summary>
/// <param name="file">A File</param>
/// <param name="stream">A Stream</param>
public sealed class FileStream(FileSystem.File.File file, Stream stream) : MetaQuant, IDisposable
{
    /// <summary>
    /// File stream
    /// </summary>
    public readonly Stream Stream = stream;
    
    /// <summary>
    /// Quantum File
    /// </summary>
    public readonly FileSystem.File.File File = file;

    public void Dispose() => Stream.Dispose();
}