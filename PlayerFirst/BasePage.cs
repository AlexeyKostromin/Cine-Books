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

        protected BasePage()
        {
            Driver = WebDriverFactory.GetDriver();
            DriverWait = WebDriverFactory.GetDriverWait();
        }

    }
}
