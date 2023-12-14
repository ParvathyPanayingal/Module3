using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1.Utilities
{
    internal class CoreCodes
    {
        protected RestClient client;
        protected ExtentReports extent;
        protected ExtentTest test;
        ExtentSparkReporter sparkReporter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReports/extent-report-" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

            extent.AttachReporter(sparkReporter);

            string? logfilepath = currdir + "/Logs/log_" +
               DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day).CreateLogger();
        }
        [SetUp]
        public void SetUp()
        {
            client = new RestClient("https://restful-booker.herokuapp.com");
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}