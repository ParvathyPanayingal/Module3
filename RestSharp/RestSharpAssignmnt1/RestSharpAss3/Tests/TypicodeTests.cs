using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using RestSharpAss3.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAss3
{
    [TestFixture]
    public class TypicodeTests : CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase(1)]
        public void GetSingleUser(int usrid)
        {
            test = extent.CreateTest("Get single user");
            Log.Information("Get single user test started");

            var request = new RestRequest("posts/" + usrid, Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response:{response.Content}");

                var userData = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(userData);
                Log.Information("User returned");
                Assert.That(userData.Id, Is.EqualTo(usrid));
                Log.Information("User id matches with fetch");
                Assert.IsNotEmpty(userData.UserId);
                Log.Information("UserId is not empty");
                Assert.IsNotEmpty(userData.Title);
                Log.Information("Title is not empty");
                Assert.IsNotEmpty(userData.Body);
                Log.Information("Body is not empty");
                Log.Information("Get Single User Test Passed all Asserts");
                test.Pass("GetSingleUser test passed");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }
        }

        [Test]
        [Order(2)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("Get All user");
            Log.Information("Get All user test started");

            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response:{response.Content}");

                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);
                Assert.NotNull(users);
                Log.Information("User returned");


            }
            catch (AssertionException)
            {
                test.Fail("GetAllUsers test failed");
            }
        }

        [Test]
        [Order(3)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Create user test started");

            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 1, id = 1, title = "John Doe", body = "Software Developer" });

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));//created will take both 200 and 201
                Log.Information($"API Response:{response.Content}");

                var userData = JsonConvert.DeserializeObject<UserData>(response.Content);

                Assert.NotNull(userData);
                Log.Information("User returned");


            }
            catch (AssertionException)
            {
                test.Fail("Create user test failed");
            }
        }

        [Test]
        [Order(4)]
        [TestCase(1)]

        public void UpdateUser(int usrid)
        {
            test = extent.CreateTest("Update user");
            Log.Information("Update user test started");

            var request = new RestRequest("posts/"+ usrid, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 1, id = 1, title = "Updated John Doe", body = "Updated Software Developer" });

            var response = client.Execute(request);
            try
            {
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Log.Information($"API Response:{response.Content}");

            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);

            Assert.NotNull(userData);
            Log.Information("User returned");


            }
            catch (AssertionException)
            {
                test.Fail("Update user test failed");
            }
        }

        [Test]
        [Order(5)]
        [TestCase(1)]

        public void DeleteUser(int usrid)
        {
            test = extent.CreateTest("Delete user");
            Log.Information("Delete user test started");

            var request = new RestRequest("posts/"+ usrid, Method.Delete);

            var response = client.Execute(request);
            try
            {
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Log.Information($"API Response:{response.Content}");

            }
            catch (AssertionException)
            {
                test.Fail("Delete user test failed");
            }
        }

        [Test]
        [Order(6)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get non existing user");
            Log.Information("Get non existing user test started");

            var request = new RestRequest("posts/999", Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information($"API Response:{response.Content}");
                Log.Information("Get non existing User Test Passed all Asserts");
                test.Pass("Get non existing User test passed");

            }
            catch (AssertionException)
            {
                test.Fail("GetNonExistingUser test failed");
            }



        }
    }
}
