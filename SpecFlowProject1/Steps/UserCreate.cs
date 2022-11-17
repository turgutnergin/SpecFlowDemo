using System.Net;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowProject1.Repository;

namespace SpecFlowProject1;

[Binding]
public class UserCreate
{
    private const string BaseAddress = "http://localhost:8080/user";
    public HttpClient Client { get; set; } = null!;
    private HttpResponseMessage Response { get; set; } = null!;

    public JsonFilesRepository JsonFilesRepo { get; }

    public UserCreate(HttpClient client, JsonFilesRepository jsonFilesRepo)
    {
        Client = client;
        JsonFilesRepo = jsonFilesRepo;
        

    }

    [Given(@"I am a client")]
    public void GivenIAmAClient()
    {
        Client = Client;

    }

    [When(@"I make a Post request with '(.*)' to '(.*)'")]
    public async Task WhenIMakeAPostRequestWithTo(string file, string userCreate)
    {
        var userJson = JsonFilesRepo.Files[file];
        var content = new StringContent(userJson, Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = await Client.PostAsync(BaseAddress, content);
        var userJsdon = JsonFilesRepo.Files[file];


    }
    
  
    [Then(@"the response status code is '(.*)'")]
    public void ThenTheResponseStatusCodeIs(int statusCode)
    {
        var expected = (HttpStatusCode)statusCode;
        Assert.AreEqual(expected, Response.StatusCode);
    }
}

