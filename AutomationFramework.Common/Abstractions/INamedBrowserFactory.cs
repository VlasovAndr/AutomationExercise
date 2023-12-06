using OpenQA.Selenium;

namespace AutomationFramework.Common.Abstractions;

public interface INamedBrowserFactory : IFactory<IWebDriver>
{
    BrowserName Name { get; }
    BrowserType Type { get; }
}

public interface IFactory<out T>
{
    T Create();
}
