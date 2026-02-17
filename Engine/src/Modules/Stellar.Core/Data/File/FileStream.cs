using Stellar.Core.Quantization;

namespace Stellar.Core.Data.File;

public sealed class FileStream : MetaQuant
{
    public Stream? Stream;
    
    

    public FileStream(File file, FileMode mode, FileAccess fileAccess)
    {
        
    }
}