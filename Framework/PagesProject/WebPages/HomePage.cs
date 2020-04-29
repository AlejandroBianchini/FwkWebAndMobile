using Core;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PagesProject.WebPages
{
    public class HomePage : PageBase
    {
        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchBox;

        public HomePage(IWebDriver driver) : base("https://www.google.com.ar")
        {
            PageInit(driver, this);
        }

        public void BuscarGoogle(string text)
        {
            searchBox.SendKeys(text);
            searchBox.SendKeys(Keys.Enter);
        }
    }
}
