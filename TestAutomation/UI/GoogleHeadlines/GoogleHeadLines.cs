using DIgiOutsourceAutomation.Helpers;
using OpenQA.Selenium;

namespace DIgiOutsourceAutomation.UI.GoogleHeadlines
{
    public class GoogleHeadLines
    {
             
        public string [] Get_AllHeadlines()
        {
            int numHeadlines = ScreenDriver.driver.FindElements(By.XPath("//h3")).Count;

            string[] Headlines = new string[numHeadlines];

            for (int x = 0; x < numHeadlines; x++)
            {
                string xpath = string.Format("(//h3)[{0}]", x+1);
                Headlines[x] = ScreenDriver.driver.FindElement(By.XPath(xpath)).Text;
            }

            Parameters.SaveParameter("Headlines", Headlines);
            return Headlines;
        }
     
        public bool Validate_Headlines()
        {
            bool success = false;

            int numHeadlines = ScreenDriver.driver.FindElements(By.XPath("//h3")).Count;
            if (numHeadlines > 3)
            {
                success = true;
            }
            return success;
        }
    }
}
