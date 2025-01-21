namespace AutomationFramework.Core.Pages.Common.Header;

public class HeaderLocators
{
    public string HeaderElement => $".//header[@id = 'header']";
    public string HeaderMenuByName(string name) => $"{HeaderElement}//a[contains(text(),'{name}')]";
}
