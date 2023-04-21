using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace PlayerFirst
{
    public abstract class BasePage
    {
        protected static IWebDriver Driver { get; set; }
        protected static WebDriverWait DriverWait { get; set; }
        public static string WorkWindowHandler { get; private set; }

        protected static TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(60);

        protected BasePage()
        {
            //PageFactory.InitElements(Driver, this);

            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("--start-maximized", "--disable-sync");
            chromeOptions.AddArguments("--disable-sync");
            chromeOptions.AddArguments("chrome.switches", "--disable-extensions");
            new DriverManager().SetUpDriver(new ChromeConfig());

            Driver = new ChromeDriver(chromeOptions);
            DriverWait = new WebDriverWait(Driver, DefaultTimeOut);
            
            if (string.IsNullOrEmpty(WorkWindowHandler))
            {
                WorkWindowHandler = Driver.CurrentWindowHandle;
            }
        }

    }
}
