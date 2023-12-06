using AutomationFramework.Common.Abstractions;
using AutomationFramework.Common.Variables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutomationFramework.Core.Configuration;

public class TestRunConfiguration
{
    public DriverConfig Driver { get; set; }
    public TargetEnvironment TargetEnvironment { get; set; }
    public FrameworkConfig Framework { get; set; }
    private ILogging log;
    private DefaultVariables defaultVar;

    public TestRunConfiguration(ILogging log, DefaultVariables defaultVar)
    {
        this.log = log;
        this.defaultVar = defaultVar;
        Configurate();
    }

    private void Configurate()
    {
        try
        {
            var configData = File.ReadAllText(defaultVar.Config);
            JObject data = JObject.Parse(configData);

            JToken driverSection = data["driver"];
            Driver = data["driver"].ToObject<DriverConfig>();
            Framework = data["framework"].ToObject<FrameworkConfig>();

            var envFromVar = Environment.GetEnvironmentVariable("TARGET_ENVIRONMENT");
            var browserFromVar = Environment.GetEnvironmentVariable("BROWSER_NAME");
            var browserTypeFromVar = Environment.GetEnvironmentVariable("BROWSER_TYPE");

            Enum.TryParse(browserFromVar, true, out BrowserName browserName);
            Enum.TryParse(browserTypeFromVar, true, out BrowserType browserType);

            Driver.BrowserName = string.IsNullOrEmpty(browserFromVar)
                                             ? Driver.BrowserName
                                             : browserName;

            Driver.BrowserType = string.IsNullOrEmpty(browserTypeFromVar)
                                             ? Driver.BrowserType
                                             : browserType;

            var effectiveTargetEnv = string.IsNullOrEmpty(envFromVar)
                                             ? Framework.Enviroment
                                             : envFromVar;

            TargetEnvironment = data["enviroment"][effectiveTargetEnv.ToLower()].ToObject<TargetEnvironment>();

            log.Information($"Selected target environment: {effectiveTargetEnv}.");
            log.Information($"Selected browser name: {Driver.BrowserName}.");
            log.Information($"Selected browser type: {Driver.BrowserType}.");

        }
        catch (JsonException ex)
        {
            log.Error($"Error deserializing configuration: " + ex.Message);
            throw new ArgumentException("Error deserializing configuration: " + ex.Message, ex);

        }
        catch (Exception ex)
        {
            log.Error($"Error while configuration: " + ex.Message);
            throw new ArgumentException("Error while configuration: " + ex.Message, ex);
        }
    }
}
