namespace AutomationFramework.Common.Variables;

public class DefaultVariables
{
    private readonly string _baseDirectory;

    public DefaultVariables()
    {
        _baseDirectory = Directory.GetParent($"{AppDomain.CurrentDomain.BaseDirectory}../../../").FullName;
    }

    public string Solution
        => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

    public string Config
        => Path.Combine(Solution, "AutomationFramework.Core", "Configuration", "Resources", "test-settings.json");

    public string UITestDataFolder
        => Path.Combine(Solution, "AutomationExerciseTests", "TestData");
}
