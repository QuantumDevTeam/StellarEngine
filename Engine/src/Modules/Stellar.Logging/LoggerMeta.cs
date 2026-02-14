using Stellar.Core.Quantization;

namespace Stellar.Logging;

public class LoggerMeta(
    bool isActive = true,
    LoggerMode mode = LoggerMode.FileAndConsole,
    Stellar.Core.Data.File.Path? loggingPath = null,
    LoggingFormats? loggingFormats = null,
    float? timeDuration = null // TODO: TimedMetaQuant
    ) : MetaQuant
{
    public bool IsActive = isActive;
    public LoggerMode Mode = mode;
    public LoggerFile? File = LoggerFile.GetOrCreate(loggingPath);
    public readonly LoggingFormats LoggingFormats = loggingFormats ?? new LoggingFormats();
}