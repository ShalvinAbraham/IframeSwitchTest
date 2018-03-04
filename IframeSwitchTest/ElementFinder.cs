using OpenQA.Selenium;
using System;
using System.Threading;

namespace IframeSwitchTest
{
    public static class ElementFinder
    {
        public static IWebElement FindInnerElement(this IWebDriver driver, By by, int retryCount = 2, int retryPauseTimeInSeconds = 30)
        {
            IWebElement elem = null;

            if (retryCount > 0)
            {
                driver.SwitchTo().DefaultContent();
                Console.WriteLine($"{DateTime.Now.ToString()} : Switching to DefaultContent");
            }

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
                    Console.WriteLine($"{DateTime.Now.ToString()} : Switching to inner iframe");
                    driver.SwitchTo().Frame(iframe);
                    elem = driver.FindInnerElement(by, 0);

                    if (elem != null)
                        break;

                    Console.WriteLine($"{DateTime.Now.ToString()} : Switching Back");
                    driver.SwitchTo().ParentFrame();
                }
            }

            if (elem == null && retryCount > 0)
            {
                Console.WriteLine($"{DateTime.Now.ToString()} : Did not find the element [{by.ToString()}], waiting for {retryPauseTimeInSeconds} sec and trying {retryCount} more times");
                Thread.Sleep(retryPauseTimeInSeconds * 1000);
                elem = driver.FindInnerElement(by, --retryCount, retryPauseTimeInSeconds);
            }

            if (elem != null)
                Console.WriteLine($"{DateTime.Now.ToString()} : Got the element [{by.ToString()}] !!");

            return elem;
        }
    }
}
