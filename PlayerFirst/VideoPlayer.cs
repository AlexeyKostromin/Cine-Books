using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using NUnit.Framework;

namespace PlayerFirst
{
    public class VideoPlayer : BasePage
    {
        //Control elements        
        const string PlayMainBtnXpath = "//button[@class = 'play-block-center__button']";
        const string PlayBlockXpath = "//div[@class = 'play-block']";


        //Video mode
        const string VideoSubsXpath = "//div[contains(@class, 'video_subtitles')]";

        const string LeftSidebarWrapperXpath = "//div[contains(@class, 'left-sidebar-wrapper currentTheme')]";
        const string ReadingModeBtnXpath = "//div[@class = 'left-sidebar']//button[contains(@class, 'icon-reading-mode btn')]";
        const string ListenModeBtnXpath = "//div[@class = 'left-sidebar']//button[contains(@class, 'icon-listen-mode btn')]";

        //Reading mode
        const string NavigationBackBtnXpath = "//div[@class = 'navigation-block']//button[@class = 'icon cn-webviewer-icon-backward btn ']";
        const string NavigationForwdBtnXpath = "//div[@class = 'navigation-block']//button[@class = 'icon cn-webviewer-icon-forward btn ']";
        const string NavigationFrameCounterXpath = "//div[@class = 'navigation-block']//div[@class = 'control frame-counter']/p";

        const string ImgReadingXpath = "//div[@class = 'image-frame no-background']/img";
        const string PlayBlockNextBtnXpath = "//div[@class = 'play-block']//button[@class = 'icon cn-webviewer-icon-next btn ']";

        //Listen mode
        const string SliderXpath = "//div[@class='rc-slider']";        
        

        public void OpenPlayBlock()
        {
            var playBlock = Driver.FindElement(By.XPath(PlayBlockXpath));
            var actions = new Actions(Driver);
            actions.MoveToElement(playBlock).Perform();
            DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PlayBlockNextBtnXpath)));           
        }

        public void PlayBlockNextBtnClick()
        {
            OpenPlayBlock();
            Driver.FindElement(By.XPath(PlayBlockNextBtnXpath)).Click();
            Thread.Sleep(100);
        }

        public void ClickNextButtonNTimes(int count)
        {
            var nextBtn = Driver.FindElement(By.XPath(PlayBlockNextBtnXpath));
            for (int i = 0; i < count; i++)
            {
                OpenPlayBlock();
                nextBtn.Click();
                Thread.Sleep(100);                
            }
        }
        public void Play()
        {
            Driver.FindElement(By.XPath(PlayMainBtnXpath)).Click();
            Thread.Sleep(500);
        }

        public void OpenLeftSidebar()
        {
            var leftSidebarWrapper = Driver.FindElement(By.XPath(LeftSidebarWrapperXpath));
            var actions = new Actions(Driver);
            actions.MoveToElement(leftSidebarWrapper).Perform();            
        }

        public void EnableReadMode()
        {
            OpenLeftSidebar();
            var btn = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ReadingModeBtnXpath)));
            btn.Click();            
        }

        public void EnableListenMode()
        {
            OpenLeftSidebar();
            var btn = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ListenModeBtnXpath)));
            btn.Click();            
            Thread.Sleep(500);
        }

        //Reading mode
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

        //Listen mode
        public double GetCurrentPlayTime()
        {            
            var track = Driver.FindElement(By.XPath(SliderXpath));
            var text = track.GetAttribute("outerHTML");
            var parsed = ParseGetSliderWidth(text);
            return parsed;
        }


        //Video mode
        public string GetSubs()
        {
            return Driver.FindElement(By.XPath(VideoSubsXpath)).Text;         

        }

        public void VerifySliderChanges()
        {
            Thread.Sleep(1000);
            var initialValue = GetCurrentPlayTime();
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                var newValue = GetCurrentPlayTime();

                Assert.AreNotEqual(initialValue, newValue, "Slider value was not updated within 5 seconds");
                initialValue = newValue;
            }
        }


        public void VerifySubsChanges()
        {
            var initialValue = GetSubs();
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                var newValue = GetSubs();
                if (newValue == null || newValue == initialValue)
                {
                    continue;
                }
                Assert.AreNotEqual(initialValue, newValue, "Subs was not shanged within 10 seconds");
                return;
            }
            throw new Exception("Subs was not shanged within 10 seconds");
        }


        protected double ParseGetSliderWidth(string input)
        {
            double result = 0;
            int startIndex = input.IndexOf("width: ") + 7;
            int endIndex = input.IndexOf("%", startIndex);

            if (startIndex != -1 && endIndex != -1)
            {
                string valueString = input.Substring(startIndex, endIndex - startIndex);
                double value = double.Parse(valueString);
                result = value;
            }
            else
            {
                Console.WriteLine("No match found");
            }
            return result;
        }


    }
}
