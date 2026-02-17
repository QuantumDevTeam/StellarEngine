using Stellar.Core.Quantization;
using Path = Stellar.Core.Data.File.Path;

namespace Stellar.Logging;

public class LoggerMeta(
    bool isActive = true,
    LoggerMode mode = LoggerMode.FileAndConsole,
    Path? loggingPath = null,
    LoggingFormats? loggingFormats = null,
    float? timeDuration = null, // TODO: TimedMetaQuant
    int? sizeDiration = null // TODO: Size duration for LoggerFile
    ) : MetaQuant
{
    public bool IsActive = isActive;
    public LoggerMode Mode = mode;
    public LoggerFile? File = LoggerFile.GetOrCreate(loggingPath);
    public readonly LoggingFormats LoggingFormats = loggingFormats ?? new LoggingFormats();
}