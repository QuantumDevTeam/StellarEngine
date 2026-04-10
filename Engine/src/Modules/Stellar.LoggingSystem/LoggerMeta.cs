using Stellar.Kernel;
using Stellar.Core.Quantization;
using Stellar.LoggingSystem.Format;

namespace Stellar.LoggingSystem;

public class LoggerMeta(
    bool isActive = true,
    LoggerMode mode = LoggerMode.FileAndConsole,
    LoggingFormats? loggingFormats = null,
    IIdentifier? identifier = null
) : MetaQuant(identifier)
{
    public bool IsActive = isActive;
    public LoggerMode Mode = mode;
    public readonly LoggingFormats LoggingFormats = loggingFormats ?? new LoggingFormats();
}