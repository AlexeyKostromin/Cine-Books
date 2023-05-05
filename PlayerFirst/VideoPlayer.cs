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

        //IWebElement PlayMainBtn => Driver.FindElement(By.XPath(PlayMainBtnXpath));
        IWebElement PlayMainBtn => DriverExt.FindElement(PlayMainBtnXpath);
        IWebElement PlayMainBtn1 => DriverExt.FindElement(By.XPath(PlayMainBtnXpath), 5);
        IWebElement PlayBlock => DriverExt.FindElement(PlayBlockXpath);

        //Video mode
        const string VideoSubsXpath = "//div[contains(@class, 'video_subtitles')]";
        const string LeftSidebarWrapperXpath = "//div[contains(@class, 'left-sidebar-wrapper currentTheme')]";
        const string ReadingModeBtnXpath = "//div[@class = 'left-sidebar']//button[contains(@class, 'icon-reading-mode btn')]";
        const string ListenModeBtnXpath = "//div[@class = 'left-sidebar']//button[contains(@class, 'icon-listen-mode btn')]";
        IWebElement VideoSubs => DriverExt.FindElement(VideoSubsXpath);
        IWebElement LeftSidebarWrapper => DriverExt.FindElement(LeftSidebarWrapperXpath);
        IWebElement ReadingModeBtn => DriverExt.FindElement(ReadingModeBtnXpath);
        IWebElement ListenModeBtn => DriverExt.FindElement(ListenModeBtnXpath);

        //Reading mode
        const string NavigationBackBtnXpath = "//div[@class = 'navigation-block']//button[@class = 'icon cn-webviewer-icon-backward btn ']";
        const string NavigationForwdBtnXpath = "//div[@class = 'navigation-block']//button[@class = 'icon cn-webviewer-icon-forward btn ']";
        const string NavigationFrameCounterXpath = "//div[@class = 'navigation-block']//div[@class = 'control frame-counter']/p";

        const string ImgReadingXpath = "//div[@class = 'image-frame no-background']/img";
        const string PlayBlockNextBtnXpath = "//div[@class = 'play-block']//button[@class = 'icon cn-webviewer-icon-next btn ']";

        IWebElement NavigationBackBtn => DriverExt.FindElement(NavigationBackBtnXpath);
        IWebElement NavigationForwdBtn => DriverExt.FindElement(NavigationForwdBtnXpath);
        IWebElement NavigationFrameCounter => DriverExt.FindElement(NavigationFrameCounterXpath);
        IWebElement ImgReading => DriverExt.FindElement(ImgReadingXpath);
        IWebElement PlayBlockNextBtn => DriverExt.FindElement(PlayBlockNextBtnXpath);


        //Listen mode
        const string SliderXpath = "//div[@class='rc-slider']";
        IWebElement Slider=> DriverExt.FindElement(SliderXpath);

        public void OpenPlayBlock()
        {
            var playBlock = PlayBlock;
            var actions = new Actions(Driver);
            actions.MoveToElement(playBlock).Perform();            
            DriverExt.WaitElementClickable(PlayBlockNextBtnXpath);
        }

        public void PlayBlockNextBtnClick()
        {
            OpenPlayBlock();            
            PlayBlockNextBtn.Click();
            Thread.Sleep(100);
        }

        public void ClickNextButtonNTimes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                OpenPlayBlock();
                PlayBlockNextBtn.Click();
                Thread.Sleep(100);                
            }
        }
        public void Play()
        {            
            PlayMainBtn.Click();
            Thread.Sleep(500);
        }

        public void OpenLeftSidebar()
        {
            var leftSidebarWrapper = LeftSidebarWrapper;
            var actions = new Actions(Driver);
            actions.MoveToElement(leftSidebarWrapper).Perform();            
        }

        public void EnableReadMode()
        {
            OpenLeftSidebar();            
            var btn = DriverExt.WaitElementClickable(ReadingModeBtnXpath);
            btn.Click();            
        }

        public void EnableListenMode()
        {
            OpenLeftSidebar();            
            var btn = DriverExt.WaitElementClickable(ListenModeBtnXpath);
            btn.Click();            
            Thread.Sleep(500);
        }

        //Reading mode
        public int GetFrameCounterCurrent()
        {            
            var navigationFrameCounter = DriverExt.WaitElementClickable(NavigationFrameCounterXpath);
            var text = navigationFrameCounter.Text;
            int index = text.IndexOf('/');
            return Convert.ToInt32(text.Substring(0, index));
        }

        public string GetImgReadingCurrent()
        {            
            var imageElement1 = DriverExt.WaitElementClickable(ImgReadingXpath);
            var scrBase64 = imageElement1.GetAttribute("currentSrc");
            return scrBase64;
        }

        public void ForvardPage()
        {            
            NavigationForwdBtn.Click();
        }

        public void BackwardPage()
        {
            NavigationBackBtn.Click();
        }

        //Listen mode
        public double GetCurrentPlayTime()
        {   
            var text = Slider.GetAttribute("outerHTML");
            var parsed = ParseGetSliderWidth(text);
            return parsed;
        }


        //Video mode
        public string GetSubs()
        {
            return VideoSubs.Text;         

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
