using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using System.Collections.Generic;
using System;

namespace DIgiOutsourceAutomation.Reporting.DashboardReportingLibrary
{
    public class Dashboard
    {
        static ExtentReports extent = new ExtentReports();
        const string dir = @"C:\DebugReport\extent.html";
        static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(dir);
        static ExtentTest feature;
        static ExtentTest scenario;
        static Dictionary<string, ExtentTest> CurrentTestSuites = new Dictionary<string, ExtentTest>();
        public enum StepStatus
        {
            Pass = 0,
            Fail = 1,
            Fatal = 2,
            Error = 3,
            Warning = 4,
            Info = 5,
            Skip = 6,
            Debug = 7

        }
        public static bool Override { get; set; } = true; //Default to false , unless set to true
        public static string[] Category { get; set; }
        private static string[] Author { get; set; }
        private static int StepCount { get; set; }

        /// <summary>
        /// Creates the report, under a project name.
        /// </summary>
        /// <param name="ReportName"></param>
        /// <param name="ProjectName"></param>
        /// <returns>reportsummaryurl</returns>
        public static string CreateReport(string ReportName, string ProjectName)
        {
            string reportString = string.Empty;

            if (!System.IO.Directory.Exists(@"C:\DebugReport\"))
            {
                System.IO.Directory.CreateDirectory(@"C:\DebugReport\");
            }

            htmlReporter.Config.ReportName = String.Format("{0}.{1}", ReportName, DateTime.Now.ToString("yyyyMMdd.HHmmss.fff"));
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            extent.AttachReporter(htmlReporter);
            reportString = dir;

            return reportString;
        }

        public static void CreateTestSuite(string TestSuiteName, string TestSuiteDescription)
        {
            try
            {
                if (!CurrentTestSuites.ContainsKey(TestSuiteName))
                {
                    feature = extent.CreateTest<Feature>(TestSuiteName, TestSuiteDescription);
                    CurrentTestSuites.Add(TestSuiteName, feature);
                }
                else
                {
                    feature = CurrentTestSuites[TestSuiteName];
                }
            }
            catch (Exception feature)
            {
                throw new Exception(feature.Message);
            }
        }

        public static void CreateTest(string TestName)
        {
            try
            {
                StepCount = 0;
                scenario = feature.CreateNode(TestName);

                Console.WriteLine(StepCount);
            }
            catch (Exception scenarioException)
            {
                throw new Exception(scenarioException.Message);
            }
        }

        public static void CleanupReport()
        {
            extent.Flush();
        }

        public static void TestResult(string result, string info)
        {
            switch (result)
            {
                case "Passed":
                    scenario.Pass("");
                    break;
                case "Failed:Error":
                    scenario.Error(info);
                    break;
                case "Failed":
                    scenario.Fail(info);
                    break;
            }

        }

        public static void LogStepStatus(StepStatus stepStatus, string Message)
        {

            try
            {
                StepCount++;
                Status status = (Status)stepStatus;
                scenario.Log(status, Message);
                Console.WriteLine(StepCount);
            }
            catch (Exception stepexception)
            {
                throw new Exception(stepexception.Message);
            }

        }

    }
}

