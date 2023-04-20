using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace PlayerFirst
{
    [TestClass]
    public class UnitTest1 : BaseFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            //WebDriver.CreateDriverChrome();
            string url = "https://cine-books.com";
            Driver.Navigate().GoToUrl(url);


            var playBnt = Driver.FindElement(By.XPath(""));
        }
    }
}
