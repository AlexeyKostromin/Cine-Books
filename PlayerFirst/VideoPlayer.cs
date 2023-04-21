using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFirst
{
    public class VideoPlayer : BasePage
    {
        //Reading mode
        const string NavigationBackBtnXpath = "//div[@class = 'navigation-block']//button[@class = 'icon cn-webviewer-icon-backward btn ']";
        const string NavigationForwdBtnXpath = "//div[@class = 'navigation-block']//button[@class = 'icon cn-webviewer-icon-forward btn ']";
        const string NavigationFrameCounterXpath = "//div[@class = 'navigation-block']//div[@class = 'control frame-counter']/p";

        const string ImgReadingXpath = "//div[@class = 'image-frame no-background']/img";



        public int GetFrameCounterCurrent()
        {
            var navigationFrameCounter = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(NavigationFrameCounterXpath)));
            var text = navigationFrameCounter.Text;
            int index = text.IndexOf('/');
            return Convert.ToInt32(text.Substring(0, index));
        }

        public string GetImgReadingCurrent()
        {
            var imageElement1 = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ImgReadingXpath)));
            var scrBase64 = imageElement1.GetAttribute("currentSrc");
            return scrBase64;
        }

        public void ForvardPage()
        {
            var navigationForwdBtn = Driver.FindElement(By.XPath(NavigationForwdBtnXpath));
            navigationForwdBtn.Click();
        }

        public void BackwardPage()
        {
            var navigationForwdBtn = Driver.FindElement(By.XPath(NavigationBackBtnXpath));
            navigationForwdBtn.Click();
        }
    }
}
