namespace Stellar.Logging;

[Flags]
public enum LoggerMode
{
    Console,
    File,
    FileAndConsole = Console | File,
}