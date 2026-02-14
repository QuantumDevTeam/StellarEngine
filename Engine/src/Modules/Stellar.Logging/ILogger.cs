using Stellar.Kernel.Quantization;

namespace Stellar.Logging;

public interface ILogger : IQuant
{
    void Log(LogLevel level, string message);
    void LogWithoutFormat(string message);
}