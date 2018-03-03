using OpenQA.Selenium;

namespace IframeSwitchTest
{
    public static class ElementFinder
    {
        public static IWebElement FindInnerElement(this IWebDriver driver, By by, bool switchDriverToDefaultContent = true)
        {
            IWebElement elem = null;

            if (switchDriverToDefaultContent)
                driver.SwitchTo().DefaultContent();

            var elems = driver.FindElements(by);

            if (elems.Count > 0)
            {
                elem = elems[0];
            }
            else
            {
                var iframes = driver.FindElements(By.TagName("iframe"));

                foreach (var iframe in iframes)
                {
                    driver.SwitchTo().Frame(iframe);
                    elem = driver.FindInnerElement(by, false);

                    if (elem != null)
                        break;

                    driver.SwitchTo().ParentFrame();
                }
            }

            return elem;
        }
    }
}
