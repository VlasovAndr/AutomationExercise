namespace AutomationFramework.Common.Services;

public class CleanupTestService
{
    private readonly List<Action> cleanupActions;

    public CleanupTestService()
    {
        cleanupActions = new List<Action>();
    }

    public void AddCleanupAction(Action cleanupAction)
    {
        cleanupActions.Add(cleanupAction);
    }

    public void Cleanup()
    {
        foreach (var action in cleanupActions)
        {
            action();
        }
    }
}