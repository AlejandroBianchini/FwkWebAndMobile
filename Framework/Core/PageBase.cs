using Core.Enums;
using Core.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void Action(Navigation.NavigationActions action)
        {
            Browser.Action(action);
        }

        public void AceptarPopUp()
        {            
            Browser.Alert.Accept();
        }

        public void CancelarPopUp()
        {
            Browser.Alert.Dismiss();
        }

        public void GoTo()
        {
            Browser.GoTo(Url);
        }

        public void Quit()
        {
            Browser.Quit();
        }

        public void PrintScreen(String fileName, ScreenshotImageFormat imageFormat, String path = null)
        {
            Browser.PrintScreen(fileName, imageFormat, path);
        }

        public IWebElement ExplicitWait(Int32 time, Func<IWebDriver, IWebElement> explicitWaitFunc)
        {
            return Browser.ExplicitWait(time, explicitWaitFunc);
        }

        public bool ExplicitWait(Int32 time, Func<IWebDriver, bool> explicitWaitFunc)
        {
            return Browser.ExplicitWait(time, explicitWaitFunc);
        }

        public static bool FindElementIfExists(By by)
        {
            return Browser.FindElementIfExists(by);
        }

        public void PressBtn(IWebElement boton)
        {
            boton.Click();
        }

        public void PressBtn(IWebElement boton1, IWebElement boton2)
        {
            boton1.Click();
            boton2.Click();
        }

        public IWebElement Find(By objeto)
        {
            return ExplicitWait(10, x => x.FindElement(objeto));
        }

        public IWebElement FindNestedElement(By objeto1, By objeto2)
        {
            return (Browser.FindNestedElements(objeto1, objeto2));
        }

        public virtual void MoveToElement(IWebElement elem)
        {
            Browser.MoveToElement(elem);
        }

        public void DragAndDrop(IWebElement elem1, IWebElement elem2)
        {
            Browser.DragAndDrop(elem1, elem2);
        }

        public virtual void CompleteField(IWebElement element, String documento)
        {
            Browser.CompleteField(element, documento);

        }

        public virtual void JavaScript(String jav)
        {
            Browser.Javas(jav);
        }

        public static string PagesSource()
        {
            return Browser.CodigoPage();
        }

        public virtual void Scroll(string i = null)
        {
            Browser.Scroll(i);
        }

        #region Windows

        public List<String> GetAllWindows()
        {
            return Browser.GetAllWindows();
        }

        public void SwitchToWindowByTitle(String title)
        {
            Browser.SwitchToWindowByTitle(title);
        }

        public void SwitchToWindowByUrl(String url)
        {
            Browser.SwitchToWindowByUrl(url);
        }

        public void SwitchToDefaultWindow()
        {
            Browser.SwitchToDefaultContent();
        }

        #endregion

        #region Frames


        public static void SwitchToDefaultFrame()
        {
            Browser.SwitchToDefaultFrame();
        }

        public void SwitchToFrame(String frameName)
        {
            Browser.SwitchToFrame(frameName);
        }

        public void SwitchToFrameElement(IWebElement frameName)
        {
            Browser.SwitchToFrameElement(frameName);
        }


        #endregion

    }
}
