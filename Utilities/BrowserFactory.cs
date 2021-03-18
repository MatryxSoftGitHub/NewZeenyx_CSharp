using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace NewZeenyx_CSharp.Utilities
{
    class BrowserFactory
    {


        public static IWebDriver OnStart(IWebDriver driver, string BrowserName, string AppURL)
        {
            switch (BrowserName)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;

                case "Firefox":
                    driver = new FirefoxDriver();
                    break;

                case "IE":
                    driver = new InternetExplorerDriver();
                    break;

                default:
                    Console.WriteLine("We do not support this browser");
                    break;
            }

            //driver.Url = AppURL;
            driver.Navigate().GoToUrl(AppURL);
            driver.Manage().Window.Maximize();
            return driver;

        }
    }
}
