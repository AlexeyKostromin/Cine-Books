using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace VideoPlayer
{
    [TestFixture]
    public class BaseFixture
    {
        private IWebDriver driver;

        //[SetUp]
        //public void Setup()
        //{
        //    var chromeOptions = new ChromeOptions();
        //    chromeOptions.AddArgument("--headless");
        //    driver = new ChromeDriver("path/to/chromedriver", chromeOptions);
        //}

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var driver = new ChromeDriver();
        }

        [Test]
        public void MyTest()
        {
            string url = "ttps://cine-books.com";
            driver.Navigate().GoToUrl(url);
        }

        [TearDown]
        public void Teardown()
        {
            // Close your WebDriver instance here
            driver.Quit();
        }
    }
}
