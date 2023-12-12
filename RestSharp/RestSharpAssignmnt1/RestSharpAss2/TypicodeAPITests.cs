using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAss2
{
    internal class TypicodeAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";


        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }


        [Test]
        [Order(0)]
        public void GetSingleUser()
        {
            var request = new RestRequest("posts/2", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(userData);
            Assert.That(userData.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(userData.UserId);
            Assert.IsNotEmpty(userData.Title);
            Assert.IsNotEmpty(userData.Body);
        }

        [Test]
        [Order(1)]
        public void GetAllUsers()
        {
            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);
            Assert.NotNull(users);
            
        }

        [Test]
        [Order(2)]
        public void CreateUser()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new {userId=1,id=1, title = "John Doe", body = "Software Developer" });

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));//created will take both 200 and 201

            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);

            Assert.NotNull(userData);
            
        }

        [Test]
        [Order(3)]

        public void UpdateUser()
        {
            var request = new RestRequest("posts/1", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 1, id = 1, title = "Updated John Doe", body = "Updated Software Developer" });

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);

            Assert.NotNull(userData);
            
        }

        [Test]
        [Order(4)]

        public void DeleteUser()
        {
            var request = new RestRequest("posts/1", Method.Delete);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

        }

        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("posts/999", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
