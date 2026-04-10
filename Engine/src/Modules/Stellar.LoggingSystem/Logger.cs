using System.Diagnostics;
using System.Globalization;
using Stellar.Core.Data.Registry;
using Stellar.Core.Quantization;
using Stellar.Kernel;
using Stellar.LoggingSystem.Format;

namespace Stellar.LoggingSystem;

/// <summary>
/// Default StellarEngine Logger
/// </summary>
/// <param name="mode">working mode</param>
/// <param name="loggingFormats">Formats of logger</param>
public sealed class Logger(
    LoggerMode mode = LoggerMode.FileAndConsole,
    LoggingFormats? loggingFormats = null
) : RegistrableQuant<ILogger, LoggerMeta>(
    new LoggerMeta(
        mode: mode,
        loggingFormats: loggingFormats
    )
), ILogger
{
    private static readonly Lock _lock = new();

    static Logger()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Title = "Quantum console";
        Console.Out.Flush();
    }

    internal static void InitDefault()
    {
        AddLogger(new Logger());
        Success("Default Logger added");
        Separator();
    }

    /// <summary>
    /// Добавляет логер в список
    /// </summary>
    /// <param name="logger">Сам логер</param>
    public static void AddLogger(ILogger logger)
    {
        QuantsRegistry<>.Instance.Register(logger);
    }

    /// <summary>
    /// Выдаёт логер по идентификатору
    /// </summary>
    /// <param name="identifier">Объект, ассоциирующеюся с логером</param>
    /// <returns>Логер, если такой нашёлся</returns>
    public static ILogger? GetLogger(IIdentifier identifier)
    {
        return QuantsRegistry<>.Instance.Get(identifier);
    }

    /// <summary>
    /// Изымает логер из списка зарегестрированных
    /// </summary>
    /// <param name="identifier">Объект, ассоциирующеюся с логером</param>
    /// <returns>Логер, если такой нашёлся</returns>
    public static ILogger? PopLogger(IIdentifier identifier)
    {
        return QuantsRegistry<>.Instance.Pop(identifier);
    }

    /// <summary>
    /// Запускает логирование
    /// </summary>
    public void Start()
    {
        Meta = true;
        Debug($"Logging started. File {Meta.File?.Info.Name}");
    }

    /// <summary>
    /// Останавливает логирование
    /// </summary>
    public void Finish()
    {
        Debug($"Logging finished. File {Meta.File?.Info.Name}");
        Meta.IsActive = false;
    }

    /// <summary>
    /// Just Console Beep 
    /// </summary>
    public static void Beep()
    {
        Console.Beep();
    }

    /// <summary>
    /// Информационный лог
    /// </summary>
    /// <param name="message">Текст с информацией</param>
    public static void Info(string message)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.Log(LogLevel.Info, message);
        }
    }

    /// <summary>
    /// Отладочный лог
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void Debug(string message)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.Log(LogLevel.Debug, message);
        }
    }

    /// <summary>
    /// Лог об удачной операции
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void Success(string message)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.Log(LogLevel.Success, message);
        }
    }

    /// <summary>
    /// Лог - предупреждение
    /// </summary>
    /// <param name="message">Текст предупреждения</param>
    public static void Warning(string message)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.Log(LogLevel.Warning, message);
        }
    }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    /// <param name="message">Сообщение о ошибке</param>
    public static void Error(string message)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.Log(LogLevel.Error, message);
        }
    }

    /// <summary>
    /// Вывод ошибки
    /// </summary>
    /// <param name="message">Сообщение в добавок к ошибке</param>
    /// <param name="e">Обрабатываемая ошибка</param>
    public static void Exception(string message, Exception e)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.Log(
                LogLevel.Exception,
                message + "\n" + e + "\n" + $"{type}: {e.Message}"
            );
        }
    }

    /// <summary>
    /// Логирует сообщение без форматирования
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void SimpleLog(string message)
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.LogWithoutFormat(message);
        }
    }

    /// <summary>
    /// Вывод пустую строку
    /// </summary>
    public static void Separator()
    {
        foreach (ILogger logger in QuantsRegistry<>.Instance.Values)
        {
            logger.LogWithoutFormat("");
        }
    }

    public void LogWithoutFormat(string message) => LogWithoutFormatAsync(message).Wait();

    private Task LogWithoutFormatAsync(string message)
    {
        WriteInConsole(LogLevel.Info, message);
        WriteInFile(message);
        return Task.CompletedTask;
    }

    public void Log(LogLevel level, string message) => LogAsync(level, message).Wait();

    private async Task LogAsync(LogLevel level, string message)
    {
        if (!Meta.IsActive) return;

        GetCalledMethodName(out var typeName, out var methodName, out var lineNumber);
        var format = Meta.LogFormat.GetFormat(level);
        var nowTime = DateTime.Now;

        await LogInConsole(
            level, message, format, nowTime,
            typeName, methodName, lineNumber);
        await LogInFile(
            level, message, format, nowTime,
            typeName, methodName, lineNumber
        );
    }

    private async Task LogInConsole(
        LogLevel level, string message, LogFormat format, DateTime nowTime,
        string typeName, string methodName, int lineNumber
    )
    {
        var formattedMessage = string.Format(
            format.ColorizedFormat,
            nowTime,
            (
                nowTime.Millisecond * 10 + float.Round(nowTime.Nanosecond / 100f, 0)
            ).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0'),
            level,
            typeName,
            methodName,
            lineNumber,
            message
        );

        await WriteInConsole(level, formattedMessage);
    }

    private Task WriteInConsole(LogLevel level, string message)
    {
        var writer = level is LogLevel.Error or LogLevel.Exception ? Console.Error : Console.Out;
        lock (_lock)
        {
            writer.WriteLineAsync(message);
            writer.FlushAsync();
        }

        return Task.CompletedTask;
    }

    private async Task LogInFile(
        LogLevel level, string message, LogFormat format, DateTime nowTime,
        string typeName, string methodName, int lineNumber)
    {
        var formattedMessage = string.Format(
            format.Format,
            nowTime,
            (
                nowTime.Millisecond * 10 + float.Round(nowTime.Nanosecond / 100f, 0)
            ).ToString(CultureInfo.InvariantCulture).PadLeft(4, '0'),
            level,
            typeName,
            methodName,
            lineNumber,
            message
        );

        await WriteInFile(formattedMessage);
    }

    private Task WriteInFile(string message)
    {
        if (Meta.FileStream is not { }) return Task.CompletedTask;
        
        await Meta.WriteLine(formattedMessage);

        return Task.CompletedTask;
    }

    private static void GetCalledMethodName(
        out string typeName, out string methodName, out int lineNumber
    )
    {
        var stackTrace = new StackTrace(skipFrames: 6, fNeedFileInfo: true);
        
        var frame = stackTrace.GetFrame(0);
        
        if (frame == null)
        {
            typeName = "UnknownType";
            methodName = "UnknownMethod";
            lineNumber = -1;
            return;
        }

        var method = frame.GetMethod();
        if (method == null)
        {
            typeName = "UnknownType";
            methodName = "UnknownMethod";
            lineNumber = -1;
            return;
        }

        typeName = method.DeclaringType?.FullName ?? "UnknownType";
        methodName = method.Name;
        lineNumber = frame.GetFileLineNumber();
    }
}