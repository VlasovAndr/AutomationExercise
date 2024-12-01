namespace AutomationFramework.Common.Services;

public class CleanupPlaywrightTestService
{
    private readonly List<Func<Task>> cleanupActions;

    public CleanupPlaywrightTestService()
    {
        cleanupActions = new List<Func<Task>>();
    }

    public void AddCleanupAction(Func<Task> cleanupAction)
    {
        cleanupActions.Add(cleanupAction);
    }

    public async Task CleanupAsync()
    {
        foreach (var action in cleanupActions.ToList())
        {
            await action();
        }
    }
}
