using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Support.UI;

namespace PlayerFirst
{
    public static class WebDriverFactory
    {
        private static IWebDriver driver;
        private static WebDriverWait driverWait { get; set; }
        private static TimeSpan defaultTimeOut = TimeSpan.FromSeconds(60);

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                var chromeOptions = new ChromeOptions();
                //chromeOptions.AddArguments("--start-maximized", "--disable-sync");
                chromeOptions.AddArguments("--disable-sync");
                chromeOptions.AddArguments("chrome.switches", "--disable-extensions");
                new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
            }
            return driver;
        }

        public static WebDriverWait GetDriverWait()
        {
            if (driverWait == null)
            {
                driverWait = new WebDriverWait(driver, defaultTimeOut);
            }
            return driverWait;
        }
    }
}
