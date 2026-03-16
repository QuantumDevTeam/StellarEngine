#pragma warning disable CS8618

namespace Stellar.Logging.Format;

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
    public LogFormat Info { get; init; }
    public LogFormat Debug { get; init; }
    public LogFormat Success { get; init; }
    public LogFormat Warning { get; init; }
    public LogFormat Error { get; init; }
    public LogFormat Exception { get; init; }

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