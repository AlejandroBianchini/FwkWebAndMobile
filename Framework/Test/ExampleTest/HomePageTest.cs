using NUnit.Framework;
using ProjectToTest;
using ProjectToTest.pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.ExamplePageTest
{
    [TestFixture]
    [Parallelizable]
    public class HomePageTest
    {
        public TestContext TestContext { get; set; }
        //private ReportHelper reportHelper = new ReportHelper();
        
        [SetUp]        
        public void Initialize()
        {
            Pages.HomePage.GoTo();
            //reportHelper.Start(TestContext);
        }

        [Test]        
        public void TestReport()
        {
            Pages.HomePage.BuscarGoogle("Selenium");

            Assert.IsTrue(true);
        }

        [Test]
        public void TestReport2()
        {
           
            Assert.IsTrue(true);
        }

        [TearDown]
        public void CleanUp()
        {
            //reportHelper.GenerateReport(TestContext);
            Pages.HomePage.Quit();
        }
    }
}
