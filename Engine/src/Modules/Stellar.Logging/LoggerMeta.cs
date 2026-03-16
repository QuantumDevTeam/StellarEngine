using Stellar.Core.Data.File;
using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.Logging.Format;

namespace Stellar.Logging;

public class LoggerMeta(
    Location loggingFileLocation,
    bool isActive = true,
    LoggerMode mode = LoggerMode.FileAndConsole,
    LoggingFormats? loggingFormats = null,
    float? timeDuration = null,
    int? sizeDuration = null, // TODO: Size duration for LoggerFile
    IIdentifier? identifier = null
) : TimedMetaQuant(identifier, timeDuration)
{
    public bool IsActive = isActive;
    public LoggerMode Mode = mode;
    public LoggerFile? File = LoggerFile.GetOrCreate(loggingFileLocation);
    public readonly LoggingFormats LoggingFormats = loggingFormats ?? new LoggingFormats();
}