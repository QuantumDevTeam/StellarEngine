using Stellar.Core.Data.File;
using Stellar.Kernel;
using File = Stellar.Core.Data.File.File;

namespace Stellar.Logging;

public class LoggerFile(Location location, IIdentifier? identifier = null) : File("LoggerFile", location, identifier), IDisposable
{
    public static LoggerFile? GetOrCreate(Location? loggingPath, int? sizeDiration = null)
    {
        throw new NotImplementedException("GetOrCreate LOGGER FILE");
    }

    public void Dispose()
    {
        // TODO: Dispose of LoggerFile
    }
}