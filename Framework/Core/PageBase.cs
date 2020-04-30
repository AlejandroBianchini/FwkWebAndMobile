using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Enums;
using OpenQA.Selenium;

namespace Core
{
    public abstract class PageBase
    {
        public String Title { get; set; }
        public String Url { get; set; }

        protected PageBase(String url)
        {            
            Url = url;
        }       

        public void PageInit(IWebDriver driver, object page)
        {
            CommonsFunctions.PageInit(driver, page);
        }

        public void KillDriverProcesses()
        {
            CommonsFunctions.KillDriverProcesses();
        }

        public void Action(Navigation.NavigationActions action)
        {
            CommonsFunctions.Action(action);
        }

        public void AceptarPopUp()
        {            
            CommonsFunctions.Alert.Accept();
        }

        public void CancelarPopUp()
        {
            CommonsFunctions.Alert.Dismiss();
        }

        public void GoTo()
        {
            CommonsFunctions.GoTo(Url);
        }

        public void Quit()
        {
            CommonsFunctions.Quit();
        }

        public void PrintScreen(String fileName, ScreenshotImageFormat imageFormat, String path = null)
        {
            CommonsFunctions.PrintScreen(fileName, imageFormat, path);
        }

        public static IWebElement ExplicitWait(int time, ExpectedConditionsEnum expected, By by)
        {
            return CommonsFunctions.ExplicitWait(time, expected, by);
        }

        public static ReadOnlyCollection<IWebElement> ExplicitWait(ExpectedConditionsEnum expected, By by)
        {
            return CommonsFunctions.ExplicitWait(expected, by);
        }

        public static Boolean ExplicitWait(int time, ExpectedConditionsEnum expected, By by, string text = "")
        {
            return CommonsFunctions.ExplicitWait(time, expected, by, text);
        }

        public static IWebElement ExplicitWait(int time, ExpectedConditionsEnum expected, IWebElement element)
        {
            return CommonsFunctions.ExplicitWait(time, expected, element);
        }

        public static Boolean ExplicitWait(int time, ExpectedConditionsEnum expected, IWebElement element, string text = "")
        {
            return CommonsFunctions.ExplicitWait(time, expected, element, text);
        }

        public static Boolean ExplicitWait(int time, ExpectedConditionsEnum expected, string text)
        {
            return CommonsFunctions.ExplicitWait(time, expected, text);
        }

        public static bool FindElementIfExists(By by)
        {
            return CommonsFunctions.FindElementIfExists(by);
        }

        public IWebElement Find(By objeto)
        {
            return ExplicitWait(3, ExpectedConditionsEnum.ElementExists, objeto);
        }

        public IWebElement FindNestedElement(By objeto1, By objeto2)
        {
            return (CommonsFunctions.FindNestedElements(objeto1, objeto2));
        }

        public virtual void MoveToElement(IWebElement elem)
        {
            CommonsFunctions.MoveToElement(elem);
        }

        public virtual void JavaScript(String jav)
        {
            CommonsFunctions.Javas(jav);
        }

        public static string PagesSource()
        {
            return CommonsFunctions.PageSource();
        }

        public virtual void Scroll(string i = null)
        {
            CommonsFunctions.Scroll(i);
        }

        #region MobileFunctions
        public static void PressAndSwipe(IWebElement element, double porcent, int loop = 0)
        {
            CommonsFunctions.PressAndSwipe(element, porcent, loop);
        }
        #endregion

        #region Windows

        public List<String> GetAllWindows()
        {
            return CommonsFunctions.GetAllWindows();
        }

        public void SwitchToWindowByTitle(String title)
        {
            CommonsFunctions.SwitchToWindowByTitle(title);
        }

        public void SwitchToWindowByUrl(String url)
        {
            CommonsFunctions.SwitchToWindowByUrl(url);
        }

        public void SwitchToDefaultWindow()
        {
            CommonsFunctions.SwitchToDefaultContent();
        }

        #endregion

        #region Frames


        public static void SwitchToDefaultFrame()
        {
            CommonsFunctions.SwitchToDefaultFrame();
        }

        public void SwitchToFrame(String frameName)
        {
            CommonsFunctions.SwitchToFrame(frameName);
        }

        public void SwitchToFrameElement(IWebElement frameName)
        {
            CommonsFunctions.SwitchToFrameElement(frameName);
        }


        #endregion

    }
}
