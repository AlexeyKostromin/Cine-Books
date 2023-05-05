using NUnit.Framework;
using System;
using System.Threading;

namespace PlayerFirst
{
    [TestFixture]
    public class PlayerTestFixture : BaseFixture
    {
        VideoPlayer playerMain;

        [Test]
        public void StartVideoTest()
        {
            HomePage homePage = new HomePage();
            homePage.OpenHomePage();
            homePage.AcceptCookie();
            homePage.ScrollToPlayer();

            playerMain = homePage.PlayerMain;

            //Video mode
            playerMain.Play();            
            this.VerifySliderChanges();

            playerMain.ClickNextButtonNTimes(2);
            this.VerifySubsChanges();

            playerMain.PlayBlockNextBtnClick();

            //Reading mode
            playerMain.EnableReadMode();

            var frameCounter1 = playerMain.GetFrameCounterCurrent();
            var imgReading1 = playerMain.GetImgReadingCurrent();

            //forvard
            playerMain.ForvardPage();
            var frameCounter2 = playerMain.GetFrameCounterCurrent();
            var imgReading2 = playerMain.GetImgReadingCurrent();

            Assert.AreEqual(frameCounter1 + 1, frameCounter2, "Page counter must be changed");
            Assert.AreNotEqual(imgReading1, imgReading2, "Images from different pages should be different");

            //backward
            playerMain.BackwardPage();
            var frameCounter3 = playerMain.GetFrameCounterCurrent();
            var imgReading3 = playerMain.GetImgReadingCurrent();

            Assert.AreEqual(frameCounter1, frameCounter3, "After backward we should get previous page counter");
            Assert.AreEqual(imgReading1, imgReading3, "After backward we should get previous image");

            //Listen Mode
            playerMain.EnableListenMode();
            this.VerifySliderChanges();
        }



        protected void VerifySliderChanges()
        {
            var initialValue = playerMain.GetCurrentPlayTime();
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                var newValue = playerMain.GetCurrentPlayTime();
                if(initialValue == newValue)
                    continue;
                Assert.AreNotEqual(initialValue, newValue, "Slider value was not updated within 5 seconds");                
            }
        }
        public void VerifySubsChanges()
        {
            var initialValue = playerMain.GetSubs();
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                var newValue = playerMain.GetSubs();
                if (newValue == null || newValue == initialValue)
                {
                    continue;
                }
                Assert.AreNotEqual(initialValue, newValue, "Subs was not shanged within 10 seconds");
                return;
            }
            throw new Exception("Subs was not shanged within 10 seconds");
        }
    }
}
