using Stellar.Kernel;
using File = Stellar.Core.Data.File.File;
using Path = Stellar.Core.Data.File.Path;

namespace Stellar.Logging;

public class LoggerFile(Path path, IIdentifier? identifier = null) : File("LoggerFile", path, identifier), IDisposable
{
    public static LoggerFile? GetOrCreate(Path? loggingPath)
    {
        throw new NotImplementedException("GetOrCreate LOGGER FILE");
    }

    public void Dispose()
    {
        // TODO: Dispose of LoggerFile
    }
}