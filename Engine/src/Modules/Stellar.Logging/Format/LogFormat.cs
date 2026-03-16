#pragma warning disable CS8618

namespace Stellar.Logging.Format;

public class LogFormatJson
{
    public string[] Formats { get; set; }
}

public readonly struct LogFormat
{
    public string Format { get; init; }
    public string ColorizedFormat { get; init; }

    private static string ToAnsiColors(string format)
    {
        return System.Text.RegularExpressions.Regex.Replace(
            format,
            @"<(\d+)>",
            "\x1b[$1m"
        );
    }

    private static string ToUnColorized(string format)
    {
        return System.Text.RegularExpressions.Regex.Replace(
            format,
            "<[^>]+>",
            ""
        );
    }

    public LogFormat(string format, string colorizedFormat)
    {
        Format = format;
        ColorizedFormat = colorizedFormat;
    }

    public LogFormat(LogFormatJson jsonFormat)
    {
        string fullFormat = string.Concat(jsonFormat.Formats);

        Format = ToUnColorized(fullFormat);
        ColorizedFormat = ToAnsiColors(fullFormat);
    }
}