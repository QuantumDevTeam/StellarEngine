using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Logging
{
    public interface ILogger
        : IRegistrableQuant
    {
        void Log(LogLevel level, string message);
        void LogWithoutFormat(string message);
    }
}
