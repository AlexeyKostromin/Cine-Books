using NUnit.Framework;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Net;

namespace PlayerFirst
{
    [TestFixture]
    public class PlayerTestFixture : BasePage
    {
        public VideoPlayer _playerMain = new VideoPlayer();
        
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


            const string LeftSidebarWrapperXpath = "//div[contains(@class, 'left-sidebar-wrapper currentTheme')]";
            const string LeftSidebarXpath = "//div[@class = 'left-sidebar']";
            const string ReadingModeBtnXpath = "//div[@class = 'left-sidebar']//button[contains(@class, 'icon-reading-mode btn')]";

            const string PlayBlockXpath = "//div[@class = 'play-block']";
            const string PlayBlockNextBtnXpath = "//div[@class = 'play-block']//button[@class = 'icon cn-webviewer-icon-next btn ']";

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
            Thread.Sleep(500);//replace to get picture

            var playBlock = Driver.FindElement(By.XPath(PlayBlockXpath));
            var playBlockNextBtn = Driver.FindElement(By.XPath(PlayBlockNextBtnXpath));
            //actions = new Actions(Driver);
            actions.MoveToElement(playBlock).Perform();
            Thread.Sleep(100);
            playBlockNextBtn.Click();
            playBlockNextBtn.Click();
            Thread.Sleep(100);

            var leftSidebarWrapper = Driver.FindElement(By.XPath(LeftSidebarWrapperXpath));
            var readingModeBtn = Driver.FindElement(By.XPath(ReadingModeBtnXpath));
            //actions = new Actions(Driver);
            actions.MoveToElement(leftSidebarWrapper).Perform();
            Thread.Sleep(100);
            readingModeBtn.Click();
            Thread.Sleep(100);

            //Reading mode
            //var navigationFrameCounter = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(NavigationFrameCounterXpath)));
            //var text = navigationFrameCounter.Text;// 2/78
            //get scr1
            //var imageElement1 = DriverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(ImgReadingXpath)));
            //var scrBase641 = imageElement1.GetAttribute("currentSrc");
            //byte[] imageBytes1 = Convert.FromBase64String(scrBase641);

            var frameCounter1 = _playerMain.GetFrameCounterCurrent();
            var imgReading1 = _playerMain.GetImgReadingCurrent();

            //forvard
            _playerMain.ForvardPage();
            var frameCounter2 = _playerMain.GetFrameCounterCurrent();
            var imgReading2 = _playerMain.GetImgReadingCurrent();

            Assert.AreEqual(frameCounter1 + 1, frameCounter2, "Page counter must be changed");
            Assert.AreNotEqual(imgReading1, imgReading2, "Images from different pages should be different");

            //backward
            _playerMain.BackwardPage();
            var frameCounter3 = _playerMain.GetFrameCounterCurrent();
            var imgReading3 = _playerMain.GetImgReadingCurrent();

            Assert.AreEqual(frameCounter1, frameCounter3, "After backward we should get previous page counter");
            Assert.AreEqual(imgReading1, imgReading3, "After backward we should get previous image");



            //var result = CompareWebImages(scrBase641, scrBase642);

            //string base64String = "/9j/4AAQSkZJRgABAQEAYABgAAD/..."; // the Base64-encoded image string
            //byte[] imageBytes = Convert.FromBase64String(base64String);
            //Bitmap image = new Bitmap(new MemoryStream(imageBytes));





            //Reading mode


        }


        bool CompareWebImages(string url1, string url2)
        {
            // retrieve images from URLs
            WebClient webClient = new WebClient();
            byte[] image1Bytes = webClient.DownloadData(url1);
            byte[] image2Bytes = webClient.DownloadData(url2);
            Bitmap image1 = new Bitmap(new MemoryStream(image1Bytes));
            Bitmap image2 = new Bitmap(new MemoryStream(image2Bytes));

            // compare images
            if (image1.Size != image2.Size) // check if images have the same dimensions
                return false;

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    if (image1.GetPixel(x, y) != image2.GetPixel(x, y)) // compare pixel values
                        return false;
                }
            }

            return true;
        }

        bool CompareWebImages2(string base64String1, string base64String2)
        {
            // retrieve images from URLs
            //WebClient webClient = new WebClient();

            //byte[] image1Bytes = webClient.DownloadData(url1);
            byte[] image1Bytes = Convert.FromBase64String(base64String1);

            //byte[] image2Bytes = webClient.DownloadData(url2);
            byte[] image2Bytes = Convert.FromBase64String(base64String2);

            Bitmap image1 = new Bitmap(new MemoryStream(image1Bytes));
            Bitmap image2 = new Bitmap(new MemoryStream(image2Bytes));
            
            // compare images
            if (image1.Size != image2.Size) // check if images have the same dimensions
                return false;

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    if (image1.GetPixel(x, y) != image2.GetPixel(x, y)) // compare pixel values
                        return false;
                }
            }

            return true;
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
