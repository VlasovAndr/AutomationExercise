using Allure.Net.Commons;
using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Common.Reports;

public class AllureReporter : ITestReporter
{
    public void AddAttachment(string name, string type, byte[] attachment)
    {
        AllureApi.AddAttachment(name, type, attachment);
    }

    public void AddInfo(string message)
    {
        AllureApi.Step($"{message}");
    }

    public void AddParameter(string paramName, string paramValue)
    {
        AllureLifecycle.Instance.UpdateStep(stepResult =>
        {
            stepResult.parameters.Add(
                new Parameter { name = paramName, value = paramValue }
            );
        });
    }
}
