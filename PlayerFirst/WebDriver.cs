using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Interactions;

namespace PlayerFirst
{
    
    public static class WebDriver
    {
        static IWebDriver driver;

        //[SetUp]
        //public void Setup()
        //{
        //    var chromeOptions = new ChromeOptions();
        //    chromeOptions.AddArgument("--headless");
        //    driver = new ChromeDriver("path/to/chromedriver", chromeOptions);
        //}

        public static IWebDriver CreateDriverChrome()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("--start-maximized", "--disable-sync");
            chromeOptions.AddArguments("--disable-sync");
            chromeOptions.AddArguments("chrome.switches", "--disable-extensions");
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver(chromeOptions);
            return driver;
        }



        //[SetUp]
        //public static void Setup()
        //{
        //    var chromeOptions = new ChromeOptions();
        //    //chromeOptions.AddArguments("--start-maximized", "--disable-sync");
        //    chromeOptions.AddArguments("--disable-sync");
        //    chromeOptions.AddArguments("chrome.switches", "--disable-extensions");
        //    new DriverManager().SetUpDriver(new ChromeConfig());
        //    driver = new ChromeDriver(chromeOptions);
        //}

        //[Test]
        //public void MyTest()
        //{
        //    string url = "https://cine-books.com";
        //    driver.Navigate().GoToUrl(url);
        //}

        //[TearDown]
        //public static void Teardown()
        //{
        //    // Close your WebDriver instance here
        //    driver.Quit();
        //}
    }
}
