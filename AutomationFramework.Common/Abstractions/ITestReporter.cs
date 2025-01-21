namespace AutomationFramework.Common.Abstractions;

public interface ITestReporter
{
    void AddInfo(string message);
    void AddParameter(string paramName, string paramValue);
    void AddAttachment(string name, string type, byte[] attachment);
    public void CreateStep(string stepName, Action stepAction);
}
