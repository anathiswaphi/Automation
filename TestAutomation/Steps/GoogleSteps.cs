using System;
using System.Threading;
using DIgiOutsourceAutomation.Helpers;
using DIgiOutsourceAutomation.UI;
using DIgiOutsourceAutomation.UI.GoogleHeadlines;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace DIgiOutsourceAutomation.Steps
{
    [Binding]
    public sealed class GoogleSteps
    {

        static GoogleHeadLines Headlines = new GoogleHeadLines();

        [Given(@"I navigate to google headlines page (.*)")]
        public void GivenINavigateToGoogleHeadlinesPage(string p0)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                ScreenDriver.Goto(p0);                
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }


        [Then(@"I validate google headlines page")]
        public void ThenIValidateGoogleHeadlinesPage()
        {
            string message = string.Empty;
            bool success = false;

            try
            {                
                success = Headlines.Validate_Headlines();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [When(@"I extract all the headlines")]
        public void WhenIExtractAllTheHeadlines()
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                Thread.Sleep(2000);
                ((IJavaScriptExecutor)ScreenDriver.driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 400)");
                Headlines.Get_AllHeadlines();
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [Then(@"I print the extracted headlines")]
        public void ThenIPrintTheExtractedHeadlines()
        {
            string message = string.Empty;
            bool success = false;

            try
            {                
                string [] headlines = Parameters.GetParameter<string[]>("Headlines");

                for (int x = 0;  x < headlines.Length; x++)
                {
                    message = message + "<br>" +  headlines[x];
                }

                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : "Error something went wrong");
            Assert.IsTrue(success, message);
        }

        [Then(@"I close the browser")]
        public void ThenICloseTheBrowser()
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                ScreenDriver.CloseTab();
                ScreenDriver.QuitDriver();
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : "Error something went wrong");
            Assert.IsTrue(success, message);
        }
    }
}
