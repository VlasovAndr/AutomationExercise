namespace AutomationFramework.Common.Abstractions;

public interface ILogging
{
    void Debug(string msg);
    void Error(string msg);
    void Warning(string msg);
    void Information(string msg);
    void SetLogLevel(string loglevel);
    void Dispose();
}
