using AutomationFramework.Common.Abstractions;
using Serilog;
using Serilog.Core;

namespace AutomationFramework.Common.Reports;

public class ConsoleLogger : ILogging
{
    private LoggingLevelSwitch _loggingLevelSwitch;

    public ConsoleLogger()
    {
        _loggingLevelSwitch = new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Debug);
        Log.Logger = new LoggerConfiguration().MinimumLevel.ControlledBy(_loggingLevelSwitch)
            .WriteTo.Console(
             outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .Enrich.WithThreadId().CreateLogger();

        Information("Logger is ConsoleLogger");
    }

    public void SetLogLevel(string loglevel)
    {
        switch (loglevel.ToLower())
        {
            case "debug":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                break;
            case "error":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Error;
                break;
            case "information":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information;
                break;
            case "fatal":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Fatal;
                break;
            default:
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                break;
        }
    }

    public void Debug(string msg) => Log.Logger.Debug(msg);

    public void Error(string msg) => Log.Logger.Error(msg);

    public void Warning(string msg) => Log.Logger.Warning(msg);

    public void Information(string msg) => Log.Logger.Information(msg);

    public void Dispose() => Log.CloseAndFlush();
}
