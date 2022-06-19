using RestSharp;
using System.Net;
using System.Text.Json;

namespace APITaskBoardTests
{
    public class ApiTests
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient("https://taskboard.nataliadimchovs.repl.co/api");
        }

        [Test]
        public void TestFirstTaskTitle()
        {
            request = new RestRequest("/tasks");
            response = this.client.Execute(request);

            var tasks = JsonSerializer.Deserialize<List<Tasks>>(response.Content);

            Assert.That(tasks[0].title, Is.EqualTo("Project skeleton"));

            Assert.That(tasks.Count, Is.GreaterThan(0));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

           
          
        }


        [Test]
        public void TestFindTaskByValidKeyword()
        {
            request = new RestRequest("/tasks/search/{keyword}");
            request.AddUrlSegment("keyword", "home");
            response = this.client.Execute(request);

            var tasks = JsonSerializer.Deserialize<List<Tasks>>(response.Content);

            Assert.That(tasks.Count, Is.GreaterThan(0));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Assert.That(tasks[0].title, Is.EqualTo("Home page"));

        }

        [Test]
        public void TestFindTaskByInvalidKeyword()
        {
            request = new RestRequest("/tasks/search/{keyword}");
            request.AddUrlSegment("keyword", "missingtask");
            response = this.client.Execute(request);

            var tasks = JsonSerializer.Deserialize<List<Tasks>>(response.Content);

            Assert.That(tasks.Count, Is.EqualTo(0));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        [Test]
        public void TestCreateTaskHoldingInvalidKeyword()
        {
            request = new RestRequest("/tasks");
            var body = new
            {
                description = "API + UI tests",
                board = "Open"
            };
            
            response = this.client.Execute(request, Method.Post);

            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Title cannot be empty!\"}"));

        }

        [Test]
        public void TestCreateTaskHoldingValidKeyword()
        {
            request = new RestRequest("/tasks");
            var body = new
            {
                title = "Add Task",
                description = "API + UI tests",
                board = "Open"
            };
            request.AddJsonBody(body);
            response = this.client.Execute(request, Method.Post);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var allTasks = this.client.Execute(request, Method.Get);

            var tasks = JsonSerializer.Deserialize<List<Tasks>>(allTasks.Content);
            var lastContact = tasks.Last();

            Assert.That(lastContact.title, Is.EqualTo(body.title));
            Assert.That(lastContact.description, Is.EqualTo(body.description));
          
        }
    }
}