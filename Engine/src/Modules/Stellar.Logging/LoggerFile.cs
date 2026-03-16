using Stellar.Core.Data.File;
using Stellar.Kernel;

namespace Stellar.Logging;

public class LoggerFile(Location location, IIdentifier? identifier = null)
    : Core.Data.File.File(location, LoggerFileType, identifier), IDisposable
{
    private StreamWriter? _writer;
    private readonly Lock _lock = new();

    public static readonly FileType LoggerFileType = new("LoggerFile");

    private void OpenWriter()
    {
        lock (_lock)
        {
            if (_writer != null) return;

            var stream = Location.Domain.FileSystem.OpenWrite(Location);
            _writer = new StreamWriter(stream) { AutoFlush = true };
        }
    }

    /// <summary>
    /// Writes a line to the log file.
    /// </summary>
    public void WriteLine(string line)
    {
        lock (_lock)
        {
            if (_writer != null)
                _writer.WriteLine(line);
            else
                throw new ObjectDisposedException(nameof(LoggerFile));
        }
    }

    /// <summary>
    /// Gets an existing logger file or creates a new one at the specified location.
    /// </summary>
    /// <param name="location">Location of the log file.</param>
    public static LoggerFile GetOrCreate(Location location)
    {
        // Ensure the domain exists and the file system supports writing.
        if (!location.Domain.FileSystem.Exists(location))
        {
            // Create an empty file by opening and closing a write stream.
            using (location.Domain.FileSystem.OpenWrite(location))
            {
                // Just create the file; stream is disposed immediately.
            }
        }

        var loggerFile = new LoggerFile(location);
        loggerFile.OpenWriter();
        return loggerFile;
    }

    public void Dispose()
    {
        lock (_lock)
        {
            _writer?.Flush();
            _writer?.Dispose();
            _writer = null;
        }
    }
}