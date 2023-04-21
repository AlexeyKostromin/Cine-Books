using NUnit.Framework;

namespace PlayerFirst
{
    [TestFixture]
    public class PlayerTestFixture : BaseFixture
    {
        
        [Test]
        public void StartVideoTest()
        {
            HomePage homePage = new HomePage();
            homePage.OpenHomePage();
            homePage.AcceptCookie();
            homePage.ScrollToPlayer();
            
            //Video mode
            homePage.PlayerMain.Play();            
            homePage.PlayerMain.VerifySliderChanges();

            homePage.PlayerMain.ClickNextButtonNTimes(2);
            homePage.PlayerMain.VerifySubsChanges();

            homePage.PlayerMain.PlayBlockNextBtnClick();

            //Reading mode
            homePage.PlayerMain.EnableReadMode();

            var frameCounter1 = homePage.PlayerMain.GetFrameCounterCurrent();
            var imgReading1 = homePage.PlayerMain.GetImgReadingCurrent();

            //forvard
            homePage.PlayerMain.ForvardPage();
            var frameCounter2 = homePage.PlayerMain.GetFrameCounterCurrent();
            var imgReading2 = homePage.PlayerMain.GetImgReadingCurrent();

            Assert.AreEqual(frameCounter1 + 1, frameCounter2, "Page counter must be changed");
            Assert.AreNotEqual(imgReading1, imgReading2, "Images from different pages should be different");

            //backward
            homePage.PlayerMain.BackwardPage();
            var frameCounter3 = homePage.PlayerMain.GetFrameCounterCurrent();
            var imgReading3 = homePage.PlayerMain.GetImgReadingCurrent();

            Assert.AreEqual(frameCounter1, frameCounter3, "After backward we should get previous page counter");
            Assert.AreEqual(imgReading1, imgReading3, "After backward we should get previous image");

            //Listen Mode
            homePage.PlayerMain.EnableListenMode();
            homePage.PlayerMain.VerifySliderChanges();
        }





    }
}
