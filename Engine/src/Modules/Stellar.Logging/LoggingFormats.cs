#pragma warning disable CS8618

namespace Stellar.Logging;

public class LoggingFormatsJson
{
    public LogFormatJson Info { get; set; }
    public LogFormatJson Debug { get; set; }
    public LogFormatJson Success { get; set; }
    public LogFormatJson Warning { get; set; }
    public LogFormatJson Error { get; set; }
    public LogFormatJson Exception { get; set; }
}

public struct LoggingFormats
{
    public LogFormat Info { get; init; } = new()
    {
        Format = "{0}:{1} | {2}      | {3}:{4}:{5} - {6}",
        ColorizedFormat = "\e[32m{0}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[2;32m{1}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[1;38m{2}\e[0m      " +
                          "\e[31m|\e[0m " +
                          "\e[36m{3}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[36m{4}\e[0m " +
                          // "\e[31m:\e[0m" +
                          // "\e[36m{5}\e[0m " +
                          "\e[31m-\e[0m " +
                          "\e[1;38m{6}\e[0m"
    };

    public LogFormat Debug { get; init; } = new()
    {
        Format = "{0}:{1} | {2}     | {3}:{4}:{5} - {6}",
        ColorizedFormat = "\e[32m{0}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[2;32m{1}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[1;33m{2}\e[0m     " +
                          "\e[31m|\e[0m " +
                          "\e[36m{3}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[36m{4}\e[0m " +
                          // "\e[31m:\e[0m" +
                          // "\e[36m{5}\e[0m " +
                          "\e[31m-\e[0m " +
                          "\e[1;33m{6}\e[0m"
    };

    public LogFormat Success { get; init; } = new()
    {
        Format = "{0}:{1} | {2}   | {3}:{4}:{5} - {6}",
        ColorizedFormat = "\e[32m{0}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[2;32m{1}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[1;32m{2}\e[0m   " +
                          "\e[31m|\e[0m " +
                          "\e[36m{3}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[36m{4}\e[0m " +
                          // "\e[31m:\e[0m" +
                          // "\e[36m{5}\e[0m " +
                          "\e[31m-\e[0m " +
                          "\e[1;32m{6}\e[0m"
    };

    public LogFormat Warning { get; init; } = new()
    {
        Format = "{0}:{1} | {2}   | {3}:{4}:{5} - {6}",
        ColorizedFormat = "\e[32m{0}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[2;32m{1}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[1;34m{2}\e[0m   " +
                          "\e[31m|\e[0m " +
                          "\e[36m{3}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[36m{4}\e[0m " +
                          // "\e[31m:\e[0m" +
                          // "\e[36m{5}\e[0m " +
                          "\e[31m-\e[0m " +
                          "\e[1;34m{6}\e[0m"
    };

    public LogFormat Error { get; init; } = new()
    {
        Format = "{0}:{1} | {2}     | {3}:{4}:{5} - {6}",
        ColorizedFormat = "\e[32m{0}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[2;32m{1}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[1;31m{2}\e[0m     " +
                          "\e[31m|\e[0m " +
                          "\e[36m{3}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[36m{4}\e[0m" +
                          // "\e[31m:\e[0m" +
                          // "\e[36m{5}\e[0m " +
                          "\e[31m-\e[0m " +
                          "\e[1;38m{6}\e[0m"
    };

    public LogFormat Exception { get; init; } = new()
    {
        Format = "{0}:{1} | {2} | {3}:{4}:{5} - {6}",
        ColorizedFormat = "\e[32m{0}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[2;32m{1}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[1;31m{2}\e[0m " +
                          "\e[31m|\e[0m " +
                          "\e[36m{3}\e[0m" +
                          "\e[31m:\e[0m" +
                          "\e[36m{4}\e[0m " +
                          // "\e[31m:\e[0m" +
                          // "\e[36m{5}\e[0m " +
                          "\e[31m-\e[0m " +
                          "\e[1;31m{6}\e[0m"
    };

    public LoggingFormats()
    {
    }

    public LoggingFormats(LoggingFormatsJson json)
    {
        Info = new LogFormat(json.Info);
        Debug = new LogFormat(json.Debug);
        Success = new LogFormat(json.Success);
        Warning = new LogFormat(json.Warning);
        Error = new LogFormat(json.Error);
        Exception = new LogFormat(json.Exception);
    }
}