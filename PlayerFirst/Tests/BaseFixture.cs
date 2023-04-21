﻿using NUnit.Framework;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;

namespace PlayerFirst
{
    [TestFixture]
    public class BaseFixture
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait DriverWait { get; set; }

        protected static TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(60);

        [SetUp]
        public void Setup()
        {
            Driver = WebDriverFactory.GetDriver();
            DriverWait = WebDriverFactory.GetDriverWait();
        }

        [TearDown]
        public void Teardown()
        {
            Driver.Quit();
        }

        

    }
}