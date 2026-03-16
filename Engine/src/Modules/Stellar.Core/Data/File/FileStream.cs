using Stellar.Core.Quantization;

namespace Stellar.Core.Data.File;

/// <summary>
/// Abstract Quantum File stream for operating with file content
/// </summary>
/// <param name="file">A File</param>
/// <param name="stream">A Stream</param>
public sealed class FileStream(File file, Stream stream) : MetaQuant, IDisposable
{
    /// <summary>
    /// File stream
    /// </summary>
    public readonly Stream Stream = stream;
    
    /// <summary>
    /// Quantum File
    /// </summary>
    public readonly File File = file;

    public void Dispose() => Stream.Dispose();
}