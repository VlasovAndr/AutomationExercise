namespace AutomationFramework.Core.Pages.Locators;

public class HeaderLocators
{
    public string HeaderElement => $"//header[@id = 'header']";
    public string HeaderMenuByName(string name) => $"{HeaderElement}//a[contains(text(),'{name}')]";
}
