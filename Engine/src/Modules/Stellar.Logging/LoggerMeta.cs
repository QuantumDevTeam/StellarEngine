using Stellar.Core.Quantization;
using Stellar.Kernel;
using Path = Stellar.Core.Data.File.Path;

namespace Stellar.Logging;

public class LoggerMeta(
    bool isActive = true,
    LoggerMode mode = LoggerMode.FileAndConsole,
    Path? loggingPath = null,
    LoggingFormats? loggingFormats = null,
    float? timeDuration = null, // TODO: TimedMetaQuant
    int? sizeDuration = null, // TODO: Size duration for LoggerFile
    IIdentifier? identifier = null
) : TimedMetaQuant(identifier, timeDuration)
{
    public bool IsActive = isActive;
    public LoggerMode Mode = mode;
    public LoggerFile? File = LoggerFile.GetOrCreate(loggingPath, sizeDuration);
    public readonly LoggingFormats LoggingFormats = loggingFormats ?? new LoggingFormats();
}