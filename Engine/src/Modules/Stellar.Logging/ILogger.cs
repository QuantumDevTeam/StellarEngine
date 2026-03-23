using Stellar.Core.Quantization;

namespace Stellar.Logging;

public interface ILogger : IRegistrableQuantInterface<ILogger, LoggerMeta>
{
    void Log(LogLevel level, string message);
    void LogWithoutFormat(string message);
}