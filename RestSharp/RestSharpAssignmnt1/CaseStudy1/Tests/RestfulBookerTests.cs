using CaseStudy1.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1
{
    internal class RestfulBookerTests : CoreCodes
    {
        [Test]
        [Order(0)]
        public void GetTokenTest()
        {
            test = extent.CreateTest("GetToken Test");
            var request = new RestRequest("/auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            try
            {
                var response = client.Execute(request);
                Console.WriteLine(response.Content);
                Assert.That(response.Content, Is.Not.Null, "Response is null");
                Log.Information("Request Intitiated");
                test.Pass("GetToken Test Passed");
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code doest match");
                Log.Information("Succees Response Recieved");
                test.Pass("Status code Test Successful");
            }
            catch (AssertionException ex)
            {
                string message = ex.Message;
                Log.Error(message);
                test.Fail(message + " GetTokenTest Fail");
            }
        }
        [Test]
        [Order(1)]
        public void GetAllBookingId()
        {
            test = extent.CreateTest("Get All Booking id test");
            var request = new RestRequest("/booking", Method.Get);
            try
            {
                var response = client.Execute(request);
                var GetBookingID = JsonConvert.DeserializeObject<List<GetBookingId>>(response.Content);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                test.Pass("Status code test pass");
                Log.Information("Status code test passed");
                Assert.That(GetBookingID[0].BookingId, Is.Not.Empty, "Booking id is Empty");
                test.Pass("Booking id data test pass");
                Log.Information("Booking id data test passed");
            }
            catch (AssertionException ex)
            {
                string message = ex.Message;
                Log.Error(message);
                test.Fail(message + " Get All Booking id  Fail");
            }
        }

        [Test]
        [Order(2)]
        public void UpdateUserTest()
        {
            test = extent.CreateTest("GetToken Test");
            var request = new RestRequest("/auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            try
            {
                var response = client.Execute(request);

                var token = JsonConvert.DeserializeObject<Cookies>(response.Content);
                var requestput = new RestRequest("/booking/10", Method.Put);
                requestput.AddHeader("Content-Type", "application/json");
                requestput.AddHeader("Accept", "application/json");
                requestput.AddHeader("Cookie", "token=" + token.Token);


                requestput.AddJsonBody(new
                {
                    firstname = "John",
                    lastname = "Smith",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
                var responseput = client.Execute(requestput);
                Assert.That(responseput.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                Console.WriteLine(responseput.Content);
            }
            catch (AssertionException ex) 
            {
                string message = ex.Message;
                Log.Error(message);
                test.Fail(message + " Update user Fail");
            }

        }

        [Test]
        [Order(3)]
        public void CreateUserTest()
        {
            try
            {

                var requestput = new RestRequest("/booking", Method.Post);
                requestput.AddHeader("Content-Type", "application/json");
                requestput.AddHeader("Accept", "application/json");
                ;


                requestput.AddJsonBody(new
                {
                    firstname = "John",
                    lastname = "Smith",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
                var responseput = client.Execute(requestput);
                var bookingObject = JsonConvert.DeserializeObject<Booking>(responseput.Content);
                var bookingDetailsObject = bookingObject.BookingDetails;
                var BookingDetails = bookingDetailsObject.BookingDate;
                Console.WriteLine(bookingDetailsObject.firstname);
                Console.WriteLine(responseput.Content);
                Assert.That(responseput.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
            }
            catch (AssertionException ex)
            {
                string message = ex.Message;
                Log.Error(message);
                test.Fail(message + " Create Fail");
            }
        }
        [Test]
        [Order(4)]
        public void DeleteUserTest()
        {

            test = extent.CreateTest("Delete Booking test");
            ;
            var requestAuth = new RestRequest("/auth", Method.Post);
            requestAuth.AddHeader("Content-Type", "application/json");
            requestAuth.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var request = new RestRequest("/booking/9", Method.Delete);
            try
            {
                var responseAuth = client.Execute(requestAuth);
                var token = JsonConvert.DeserializeObject<Cookies>(responseAuth.Content);
                request.AddHeader("Cookie", "token=" + token.Token);
                var response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), "Status code is not 200");
                test.Pass("Status code test pass");
                Log.Information("Status code test passed");
                test.Pass("Booking id data test pass");
                Log.Information("Booking id data test passed");
            }
            catch (AssertionException ex)
            {
                string message = ex.Message;
                Log.Error(message);
                test.Fail(message + " Delete Fail");
            }
        }


    }
}