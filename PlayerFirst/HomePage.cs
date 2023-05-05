using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace PlayerFirst
{
    public class HomePage : BasePage
    {
        const string homePaheUrl = "https://cine-books.com";

        public VideoPlayer PlayerMain;

        const string AcceptCookieBtnXpath = "//button[@id='gdpr-cookie-accept']";
        const string GdprCookieMessageXpath = "//div[@id='gdpr-cookie-message']";
        const string FormatAdvantagesLnkXpath = "//a[text()='format advantages']";
        
        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(homePaheUrl);
            PlayerMain = new VideoPlayer();            
        }

        public void ScrollToPlayer()
        {
            var formatAdvantagesLnk = DriverExt.WaitElementClickable(FormatAdvantagesLnkXpath);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(formatAdvantagesLnk, 0, -1000).Perform();
            Thread.Sleep(500);
        }

        public void AcceptCookie()
        {
            var acceptCookieBtn = DriverExt.WaitElementClickable(AcceptCookieBtnXpath);
            acceptCookieBtn.Click();
            DriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(GdprCookieMessageXpath)));
        }


    }
}
