using Stellar.Core.Data.File;
using Stellar.Kernel;

namespace Stellar.Logging;

public class LoggerFile(Location location, IIdentifier? identifier = null)
    : Core.Data.File.File(location, new FileType("LoggerFile"), identifier), IDisposable
{
    public static LoggerFile? GetOrCreate(Location loggingPath,
        int? sizeDiration = null, DateTimeOffset? timeduration = null)
    {
        // TODO: Create File (read file info `needed`)
    }

    public void Dispose()
    {
        // TODO: Dispose of LoggerFile
    }
}