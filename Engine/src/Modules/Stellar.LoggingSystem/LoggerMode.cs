namespace Stellar.LoggingSystem;

[Flags]
public enum LoggerMode : byte
{
    File = 1,
    Console = 2,

    FileAndConsole = File | Console
}