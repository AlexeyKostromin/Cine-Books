using NUnit.Framework;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace PlayerFirst
{
    [TestFixture]
    public class PlayerTestFixture : BaseFixture
    {
        [Test]
        public void StartVideoTest()
        {
            //WebDriver.CreateDriverChrome();
            string url = "https://cine-books.com";
            Driver.Navigate().GoToUrl(url);

            const string PlayBtnXpath = "//button[@class = 'play-block-center__button']";
            const string FormatAdvantagesLnkXpath = "//a[text()='format advantages']";
            const string AcceptCookieBtnXpath = "//button[@id='gdpr-cookie-accept']";
            const string GdprCookieMessageXpath = "//div[@id='gdpr-cookie-message']";
            
            var acceptCookieBtn = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(AcceptCookieBtnXpath)));
            acceptCookieBtn.Click();
            DriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(GdprCookieMessageXpath)));

            //var formatAdvantagesLnk = DriverWait.Until(drv => drv.FindElement(By.XPath(FormatAdvantagesLnkXpath)));

            var formatAdvantagesLnk = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(FormatAdvantagesLnkXpath)));
            
            var playBtn = Driver.FindElement(By.XPath(PlayBtnXpath));
           
            Actions actions = new Actions(Driver);
            actions.MoveToElement(formatAdvantagesLnk, 0, -1000).Perform();
            Thread.Sleep(500);

            playBtn.Click();
                        


        }

        public void WaitUntilButtonIsClickable(By locator)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddSeconds(10);

            while (startTime < endTime)
            {
                try
                {
                    var button = Driver.FindElement(locator);

                    if (button.Enabled && button.Displayed)
                    {
                        button.Click();
                        return;
                    }
                }
                catch (NoSuchElementException)
                {
                    // Element not found, continue waiting
                }

                // Wait for a short interval before trying again
                System.Threading.Thread.Sleep(500);
            }

            throw new TimeoutException($"Element with locator {locator} was not clickable within {endTime} seconds.");
        }

        public void WaitFormClosed()
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool formClosed = DriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("form-container")));

            if (formClosed)
            {
                Console.WriteLine("Form closed successfully.");
            }
            else
            {
                Console.WriteLine("Form did not close within the allotted time.");
            }
        }


    }
}
