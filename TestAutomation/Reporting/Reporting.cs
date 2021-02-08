
using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using DIgiOutsourceAutomation.UI;
using DIgiOutsourceAutomation.Reporting.DashboardReportingLibrary;
using DIgiOutsourceAutomation.Helpers;

namespace DIgiOutsourceAutomation.Reporting
{
    [Binding]
    public sealed class Reporting
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;

        [BeforeTestRun]
        /* initializing test report directory*/
        public static void Init()
        {
            Parameters.SaveParameter("Project","Automation");
            Parameters.SaveParameter(Parameters.Test.ApplicationName.ToString(), "Automation");
            Parameters.SaveParameter(Parameters.Test.Country.ToString(), "SA");
            Parameters.SaveParameter(Parameters.Test.MachineId.ToString(), System.Environment.MachineName.ToString());
            Parameters.SaveParameter("GUID", Guid.NewGuid().ToString());
            Parameters.SaveParameter("TestEnvironment", "UAT");
            Parameters.SaveParameter("ExtentReportScreenshotPath", "C:/Temp/HTML/Images");

            var htmlReporter = new ExtentHtmlReporter(@"C:\extentreport\ExtentReport.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void AfterRun()
        {
            extent.Flush();
            Dashboard.CleanupReport();
        }

        [BeforeFeature]
        public static void Beforefeature()
        {
            Init();
            string NewReportId = Dashboard.CreateReport(Parameters.GetParameter<string>("Project"), Parameters.GetParameter<string>("TestEnvironment"));
            Parameters.SaveParameter(Parameters.Test.NewReportId.ToString(), NewReportId);
            Dashboard.CreateTestSuite(FeatureContext.Current.FeatureInfo.Title, FeatureContext.Current.FeatureInfo.Description);
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            Dashboard.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
            var browser = NUnit.Framework.TestContext.Parameters["browser"] ?? "Chrome";
            Parameters.SaveParameter(Parameters.Test.TestName.ToString(), String.Format("{0} - {1}", FeatureContext.Current.FeatureInfo.Title, ScenarioContext.Current.ScenarioInfo.Title));
            ScreenDriver.Initialize(browser);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ScreenDriver.QuitDriver();
        }

        [BeforeStep]
        public void BeforeStep()
        {
            
        }

        [AfterStep]
        public void AfterStep()
        {

            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text + "<br>" + Parameters.GetParameter<string>("Expected"));
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text + "<br>" + Parameters.GetParameter<string>("Expected"));
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text + "<br>" + Parameters.GetParameter<string>("Expected"));
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text + "<br>" + Parameters.GetParameter<string>("Expected") );
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }

            if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }

        }
    }
}
