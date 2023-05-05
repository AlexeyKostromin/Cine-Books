using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PlayerFirst
{
    public abstract class BasePage
    {
        protected static IWebDriver Driver { get; set; }
        protected static WebDriverWait DriverWait { get; set; }

        protected static TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(60);
        protected static WebDriverExtentions DriverExt { get; set; }

        protected BasePage()
        {
            Driver = WebDriverFactory.GetDriver();
            DriverWait = WebDriverFactory.GetDriverWait();
            DriverExt = new WebDriverExtentions();
        }

        public static void RefreshCurrentTab()
        {
            Driver.Navigate().Refresh();
        }

        public static void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}
