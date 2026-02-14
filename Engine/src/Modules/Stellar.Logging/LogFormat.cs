namespace Stellar.Logging;

public readonly struct LogFormat
{
    public required string Format { get; init; }
    public required string ColorizedFormat { get; init; }
}