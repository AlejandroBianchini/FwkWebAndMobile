using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Core.Helpers
{
    public class ReportHelper
    {
        private static AppSettingsConfiguration config = new AppSettingsConfiguration(); 
        private static String filePath = config.HtmlReportsPath;
        private static String fileName = String.Format("Reporte {0}.html", DateTime.Now.ToString("dd MMM yyyy HHmmss"));
        private static string ReporteImgPath = @"img\";
        private static String imgPath = $@"{filePath}{fileName}\{ReporteImgPath}";
        private static readonly ExtentHtmlReporter htmlReport = new ExtentHtmlReporter($@"{filePath}{fileName}\{fileName}");
        public AventStack.ExtentReports.ExtentReports _instance =  new AventStack.ExtentReports.ExtentReports();
        public TestContext TestContext { get; set; }
        List<List<string>> imagesAndDetails = new List<List<string>>();

        public ReportHelper()
        {
            _instance.AttachReporter(htmlReport);
            this.ConfigureDirectories();
        }        

        protected AventStack.ExtentReports.ExtentReports ExtentReport => _instance;

        private Collection<ExtentTest> testsCollection = new Collection<ExtentTest>();
        ExtentTest test;
        string testName;

        public void StartTest(TestContext testContext)
        {
            string testName = testContext.Test.Name.Replace('"', '_').Replace("(", "").Replace(")","").Trim();
            test = ExtentReport.CreateTest(testName);
        }

        public void AddTestToReport(TestContext testContext)
        {            
            var status = testContext.Result.Outcome.Status;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");

            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            var testClassName = testContext.Test.ClassName.Split('.').Last();
            test.AssignCategory(testClassName);
            test.Log(logstatus, "El test ha finalizado con estado " + logstatus);

            foreach (var item in imagesAndDetails)
            {
                test.Log(Status.Info, item[1], MediaEntityBuilder.CreateScreenCaptureFromPath(ReporteImgPath + item[0]).Build());
            }

            if (logstatus == Status.Fail || logstatus == Status.Warning)
            {
                try
                {
                    var _fileName = String.Format("errorTest_{0}_{1}", testName, DateTime.Now.ToString("yyyyMMdd_HHmm"));
                    CommonsFunctions.PrintScreen(_fileName, ScreenshotImageFormat.Jpeg, imgPath);
                    var file = ReporteImgPath + _fileName + "." + ScreenshotImageFormat.Jpeg;
                    test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(file));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            testsCollection.Add(test);
        }

        public void GenerateReport()
        {
            foreach (var item in testsCollection)
            {
                ExtentReport.Flush();
            }
        }

        //public void GenerateReport(Collection<TestContext> testsCollection, string TestName = "")
        //{

        //    foreach (var item in testsCollection)
        //    {


        //        ExtentTest test;

        //        if (!string.IsNullOrEmpty(TestName))
        //            test = ExtentReport.CreateTest(testContext.Test.MethodName + " - " + TestName);
        //        else
        //            test = ExtentReport.CreateTest(item.Test.MethodName);


        //        var status = item.Result.Outcome.Status;
        //        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");

        //        Status logstatus;

        //        switch (status)
        //        {
        //            case TestStatus.Failed:
        //                logstatus = Status.Fail;
        //                break;
        //            case TestStatus.Inconclusive:
        //                logstatus = Status.Warning;
        //                break;
        //            default:
        //                logstatus = Status.Pass;
        //                break;
        //        }

        //        var testClassName = item.Test.ClassName.Split('.').Last();
        //        test.AssignCategory(testClassName);
        //        test.Log(logstatus, "El test ha finalizado con estado " + logstatus);

        //        foreach (var ite in imagesAndDetails)
        //        {
        //            test.Log(Status.Info, ite[1], MediaEntityBuilder.CreateScreenCaptureFromPath(ReporteImgPath + ite[0]).Build());
        //        }

        //        if (logstatus == Status.Fail || logstatus == Status.Warning)
        //        {
        //            try
        //            {
        //                var _fileName = String.Format("errorTest_{0}_{1}", item.Test.Name, DateTime.Now.ToString("yyyyMMdd_HHmm"));
        //                Browser.PrintScreen(_fileName, ScreenshotImageFormat.Jpeg, imgPath);
        //                var file = ReporteImgPath + _fileName + "." + ScreenshotImageFormat.Jpeg;
        //                test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(file));
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }

        //        ExtentReport.Flush();

        //    }
        //}

        private void ConfigureDirectories()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }
        }

        /// <summary>
        /// Add screenshot of the current execution moment.
        /// Can add details as string (optionally).
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="details"></param>
        public void AddScreenCaptureToStep(string imageName, string details = "")
        {
            ConfigureDirectories();
            CommonsFunctions.PrintScreen(imageName, ScreenshotImageFormat.Jpeg, imgPath);

            List<string> imgDetails = new List<string>();
            imgDetails.Add(imageName + ".jpeg");
            imgDetails.Add(details);
            imagesAndDetails.Add(imgDetails);
        }

    }
}
