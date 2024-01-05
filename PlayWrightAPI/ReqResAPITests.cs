using Microsoft.Playwright;
using System.Text.Json;

namespace PlayWrightAPI
{
    public class ReqResAPITests
    {
        IAPIRequestContext requestContext;
        [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            requestContext = await playwright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://reqres.in/api/",
                    IgnoreHTTPSErrors=true
                });
        }

        [Test]
        public async Task GetAllUsersTest()
        {
            var getresponse = await requestContext.GetAsync(url: "user?page=2");
            await Console.Out.WriteLineAsync("Res : \n"+getresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + getresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + getresponse.StatusText);

          
            Assert.That(getresponse.Status.Equals(200));
            Assert.That(getresponse, Is.Not.Null);
            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n" + responseBody.ToString());

        }
        [Test,TestCase("2")]
        public async Task GetSingleUserTest(int id)
        {
            var getresponse = await requestContext.GetAsync(url: "users/"+id);
            await Console.Out.WriteLineAsync("Res : \n" + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + getresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + getresponse.StatusText);


            Assert.That(getresponse.Status.Equals(200));
            Assert.That(getresponse, Is.Not.Null);
            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n" + responseBody.ToString());

        }
        [Test]
        public async Task GetSingleUserNotFound()
        {
            var getresponse = await requestContext.GetAsync(url: "users/23");
            await Console.Out.WriteLineAsync("Res : \n" + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + getresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + getresponse.StatusText);


            Assert.That(getresponse.Status.Equals(404));
            Assert.That(getresponse, Is.Not.Null);
            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n" + responseBody.ToString());
            Assert.That(responseBody.ToString(), Is.EqualTo("{}"));

        }
        [Test,TestCase("John","leader")]
        public async Task CreateUsersTest(string name,string title)
        {
            var postData = new
            {

                name = name,
                job = title
            };
            var jsonData=System.Text.Json.JsonSerializer.Serialize(postData);
            var postresponse = await requestContext.PostAsync(url: "users",
                new APIRequestContextOptions { Data = jsonData });


            await Console.Out.WriteLineAsync("Res : \n" + postresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + postresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + postresponse.StatusText);


            Assert.That(postresponse.Status.Equals(201));
            Assert.That(postresponse, Is.Not.Null);
            

        }
        [Test]
        public async Task UpdateUsersTest()
        {
            var putData = new
            {

                name = "Toby",
                job = "Software Engineer"
            };
            var jsonData = System.Text.Json.JsonSerializer.Serialize(putData);
            var putresponse = await requestContext.PutAsync(url: "users/2",
                new APIRequestContextOptions { Data = jsonData });


            await Console.Out.WriteLineAsync("Res : \n" + putresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + putresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + putresponse.StatusText);


            Assert.That(putresponse.Status.Equals(200));
            Assert.That(putresponse, Is.Not.Null);


        }
        [Test]
        public async Task DeleteUser()
        {

            var deltresponse = await requestContext.DeleteAsync(url: "users/2");
                


            await Console.Out.WriteLineAsync("Res : \n" + deltresponse.ToString());
            await Console.Out.WriteLineAsync("Code : \n" + deltresponse.Status);
            await Console.Out.WriteLineAsync("Text : \n" + deltresponse.StatusText);


            Assert.That(deltresponse.Status.Equals(204));
            Assert.That(deltresponse, Is.Not.Null);


        }

    }
}