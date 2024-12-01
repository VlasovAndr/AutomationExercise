using Microsoft.Playwright;

namespace AutomationFramework.Core.Driver
{
    public class PlaywrightDriver
    {
        public IPage CreateBrowser()
        {
            var playwright = Playwright.CreateAsync().Result;
            var browser = playwright.Chromium.LaunchAsync(new()
            {
                Headless = false
            }).Result;

            var page = browser.NewPageAsync().Result;
            Console.WriteLine("PlaywrightDriver class. CreateBrowser has been executed");

            return page;
        }
    }
}