using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace IframeSwitchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://sat4star.wixsite.com/cycle/product-page/i-m-a-product-3");
            driver.Manage().Window.Maximize();

            Thread.Sleep(10000);

            var subscriberEmail = driver.FindInnerElement(By.Name("subscriberEmail"));
            subscriberEmail.SendKeys("Test Email");

            var comment = driver.FindInnerElement(By.Name("comment"));
            comment.SendKeys("Test Comment");

            var nonExistingElement = driver.FindInnerElement(By.Name("nonExistingElement"), 10, 1);

            Console.WriteLine("Done. Press any key...");
            Console.Read();

            driver.Quit();
        }
    }
}
