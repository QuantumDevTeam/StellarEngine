using Stellar.Core.Quantization;
using Stellar.FileSystem;
using Stellar.Kernel;
using File = Stellar.FileSystem.File;
using FileStream = Stellar.FileSystem.FileStream;

namespace Stellar.LoggingSystem;

public class LoggerFile(
    Location location,
    float timeDuration = 0,
    uint sizeDuration = 0,
    DateTime? startAt = null,
    IIdentifier? identifier = null
) : TimedMetaQuant(startAt ?? DateTime.UtcNow, timeDuration, identifier), IDisposable
{
    public static readonly FileType LoggerFileType = new("LoggerFile");

    public Location FileLocation => location;
    public uint SizeDuration => sizeDuration;

    public FileStream? FileStream { get; private set; }

    private StreamWriter? _writer;
    private readonly Lock _lock = new();

    internal void Prepare()
    {
        lock (_lock)
        {
            if (_writer != null) return;
            FileStream ??= new File(location, LoggerFileType).OpenWrite();

            RessetLifetime();
            _writer = new StreamWriter(FileStream.Stream) { AutoFlush = true };
        }
    }

    internal void Free()
    {
        lock (_lock)
        {
            _writer?.Flush();
            _writer?.Dispose();
            _writer = null;

            FileStream?.Dispose();
            FileStream = null;
        }
    }

    private bool IsSizeDurationValid(string line)
    {
        return FileStream?.File.GetInfo().Length + line.Length < SizeDuration;
    }

    /// <summary>
    /// Flush all changes and recreate file sources
    /// </summary>
    public void FlushAndRecreate()
    {
        lock (_lock)
        {
            Free();
            Prepare();
        }
    }

    /// <summary>
    /// Writes a line to the log file.
    /// </summary>
    public void WriteLine(string line)
    {
        lock (_lock)
        {
            if (IsExpired || !IsSizeDurationValid(line))
            {
                FlushAndRecreate();
            }

            if (_writer != null)
                _writer.WriteLine(line);
            else
                throw new ObjectDisposedException(nameof(LoggerFile));
        }
    }

    public void Dispose()
    {
    }
}