using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

//Modularized code

GetAllUsers(client);
GetSingleUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);


//GET All Users
static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("posts", Method.Get);

    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET Response: \n" + getUserResponse.Content);
}

//GET Single User
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/2", Method.Get);

    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse Json response content
        JObject? userJson = JObject.Parse(getUserResponse.Content);

        string? userId = userJson["userId"].ToString();
        string? id = userJson["id"].ToString();
        string? title = userJson["title"].ToString();
        string? body = userJson["body"].ToString();

        Console.WriteLine("\n Get user response:");
        Console.WriteLine($"User id: {userId} \n id:{id} \n title:{title} \n body:{body}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }
}
////POST
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { userId = "45",id="5" , title ="Hello World",body="Hi hello"});

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Response: \n" + createUserResponse.Content);

}

////PUT
static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/5", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new { userId = "47",title = "Updated Have a nice day", body = "Updated Hi hello" });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Response: \n" );
    if (updateUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject? userJson = JObject.Parse(updateUserResponse?.Content);

        string? userId = userJson["userId"].ToString();
        string? title = userJson["title"].ToString();
        string? body = userJson["body"].ToString();
        Console.WriteLine($"Updated : {userId}, {title}, {body}");
    }
    else
    {
        Console.WriteLine($"Error:{updateUserResponse.ErrorMessage}");
    }
}

////DELETE
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/5", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    if (deleteUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        Console.WriteLine("\n DELETE Response:");
        Console.WriteLine("\n Deleted Successfully");
    }
    else
    {
        Console.WriteLine($"Error: {deleteUserResponse.ErrorMessage}");
    }
}

