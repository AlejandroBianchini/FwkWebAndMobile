using Core;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ProjectToTest.pages
{
    public class HomePage : PageBase
    {
        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchBox;

        public HomePage() : base("https://www.google.com.ar")
        {
            
        }

        public void BuscarGoogle(string text)
        {
            searchBox.SendKeys(text);
            searchBox.SendKeys(Keys.Enter);
        }
    }
}
