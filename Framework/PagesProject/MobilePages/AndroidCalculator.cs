using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using SeleniumExtras.PageObjects;

namespace PagesProject.MobilePages
{
    public class AndroidCalculator : PageBase
    {
        #region Elements

        [FindsBy(How = How.Id, Using = "com.android.calculator2:id/digit_0")]
        private IWebElement btn0;
        [FindsBy(How = How.Id, Using = "com.android.calculator2:id/digit_5")]
        private IWebElement btn5;
        [FindsBy(How = How.Id, Using = "com.android.calculator2:id/digit_6")]
        private IWebElement btn6;
        [FindsBy(How = How.Id, Using = "com.android.calculator2:id/eq")]
        private IWebElement btnEqual;
        [FindsBy(How = How.Id, Using = "com.android.calculator2:id/op_add")]
        private IWebElement Btnplus;
        [FindsBy(How = How.Id, Using = "com.android.calculator2:id/result")]
        private IWebElement result;

        #endregion

        public AndroidCalculator(AppiumDriver<AppiumWebElement> driver) : base("https://www.google.com.ar")
        {
            PageInit(driver, this);
        }

        public void RealizarSuma()
        {
            btn0.Tap();
            btn6.Tap();
            Btnplus.Tap();
            btn5.Tap();
            btnEqual.Click();            
        }

        public string ValidarCuenta()
        {
            return result.Text;
        }

    }
}
