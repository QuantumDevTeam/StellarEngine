using Stellar.Core.Quantization;

namespace Stellar.Core.Data.File;

public sealed class FileStream(File file, Stream stream) : MetaQuant, IDisposable
{
    public readonly Stream Stream = stream;
    public readonly File File = file;

    public void Dispose() => Stream.Dispose();
}