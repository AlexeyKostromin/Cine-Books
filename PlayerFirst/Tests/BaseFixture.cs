using NUnit.Framework;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;

namespace PlayerFirst
{
    [TestFixture]
    public class BaseFixture : BasePage
    {
        [TearDown]
        public void Teardown()
        {
            CloseBrowser();
        }

        

    }
}
