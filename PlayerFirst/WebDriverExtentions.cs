using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace PlayerFirst
{
    public class WebDriverExtentions
    {
        private IWebDriver Driver { get; set; }
        private WebDriverWait DriverWait { get; set; }


        public WebDriverExtentions()
        {
            Driver = WebDriverFactory.GetDriver();
            DriverWait = WebDriverFactory.GetDriverWait();
        }


        public IWebElement FindElement(By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d => d.FindElement(locator));
        }

        public IWebElement FindElement(string xpath)
        {
            return Driver.FindElement(By.XPath(xpath));
        }

        public IWebElement WaitElementClickable(string xpath)
        {
            return DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
        }
        
        


    }
}
