using DIgiOutsourceAutomation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace DIgiOutsourceAutomation.UI
{
    public static class ScreenDriver
    {
        public static IWebDriver driver = null;

        public enum Browsers
        {
            Chrome,
            Firefox
        }

        public static void Initialize(Browsers browsers)
        {
            NodeStartUp(browsers);
            driver.Manage().Window.Maximize();
            driver.SwitchTo().Alert().Dismiss();
        }

        public static void DismissAlerts()
        {
            driver.SwitchTo().Alert().Dismiss();
        }

        public static void Initialize(String browsers)
        {
            var Selection = Get_Browser(browsers);
            NodeStartUp(Selection);
            driver.Manage().Window.Maximize();
        }

        public static void Goto(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        private static void NodeStartUp(Browsers _browsers)
        {
            try
            {
                DesiredCapabilities caps = new DesiredCapabilities();
                switch (_browsers)
                {
                    case Browsers.Firefox:
                        driver = null;
                        break;
                    case Browsers.Chrome:
                        driver = new ChromeDriver(@"C:\BrowserNodes\");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Automation Setup Exception : {0}", ex.Message));
            }
        }
                
        public static string TakeScreenShot()
        {
            string path = Parameters.GetParameter<string>("ExtentReportScreenshotPath");
            string image = string.Format("{0}_{1}.png", "Test", Guid.NewGuid().ToString());
            path = string.Format("{0}/{1}", path, image);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);

            return Image.FromFile(path).ToString();
        }

        public static void CloseBrowser()
        {    
            driver.Close();
        }

        public static void QuitDriver()
        {
            driver.Quit();
        }
       

        private static Browsers Get_Browser(string browsers)
        {
            Browsers selection;

            switch (browsers.ToLower())
            {
                case "chrome":
                    selection = Browsers.Chrome;
                    break;
                case "firefox":
                    selection = Browsers.Firefox;
                    break;
                default:
                    Console.WriteLine("Defaulting browser to Chrome");
                    selection = Browsers.Chrome;
                    break;
            }
            return selection;
        }

        public static void SwitchToTab()
        {
            ReadOnlyCollection<string> WindowHandles = driver.WindowHandles;
            foreach (string item in WindowHandles)
            {
                driver.SwitchTo().Window(item);
                string browserTitle = driver.Title;
                string browserPageSource = driver.PageSource;
                string browserURL = driver.Url;
            }
        }

        /*Close current browser and switch to the other tab*/
        public static void CloseTab()
        {
            var tabs = driver.WindowHandles;
            var winds = tabs.Count;

            if (tabs.Count > 1)
            {
                driver.SwitchTo().Window(tabs[winds - 1]);
                driver.Close();
                driver.SwitchTo().Window(tabs[winds - 2]);
            }
        }

    }
}

