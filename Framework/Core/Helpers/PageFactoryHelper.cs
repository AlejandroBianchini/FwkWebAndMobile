using SeleniumExtras.PageObjects;

namespace Core.Helpers
{
    public static class PageFactoryHelper
    {
        public static T InitElements<T>() where T : PageBase, new()
        {
            T page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }
    }
}
