using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAss3.Utilities
{
    public class CoreCodes
    {
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        protected RestClient client;

        protected ExtentReports extent;
        protected ExtentTest test;
        ExtentSparkReporter sparkReporter;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReports/extent-report-"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

            extent.AttachReporter(sparkReporter);

            string? logfilepath = currdir + "/Logs/log_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }
        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }


    }
}
