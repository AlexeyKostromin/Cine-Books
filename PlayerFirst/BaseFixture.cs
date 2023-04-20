using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PlayerFirst
{
    public class BaseFixture
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait DriverWait { get; set; }

        protected static TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(60);

        [SetUp]
        public void Setup()
        {
            Driver = WebDriver.CreateDriverChrome();
            DriverWait = new WebDriverWait(Driver, DefaultTimeOut);
        }

        [TearDown]
        public void Teardown()
        {
            // Close your WebDriver instance here
            Driver.Quit();
        }

        

    }
}
