using AutomationFramework.Common.Abstractions;

namespace AutomationFramework.Core.Configuration;

public class TargetEnvironment
{
    public string Url { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class DriverConfig
{
    public BrowserName BrowserName { get; set; }
    public BrowserType BrowserType { get; set; }
    public string GridHubUrl { get; set; }
    public int WaitSeconds { get; set; }
    public bool Headless { get; set; }
}

public class FrameworkConfig
{
    public string Enviroment { get; set; }
    public string DownloadedLocation { get; set; }
    public string LogLevel { get; set; }
}
