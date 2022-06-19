using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTaskBoardTests
{
    public class SeleniumTests
    {

        private WebDriver driver;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://taskboard.nataliadimchovs.repl.co/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void Shutdown()
        {
            this.driver.Quit();

        }

        [Test]
        public void TestFirstTaskTitle()
        {
            var taskBoardButton = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(2) > a"));
            taskBoardButton.Click();

            var firstDoneTaskTitle = driver.FindElement(By.CssSelector("#task1 > tbody > tr.title > td")).Text;

            Assert.That(firstDoneTaskTitle, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void TestFindTaskByValidKeyword()
        {
            var searchButton = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a"));
            searchButton.Click();
            var keywordField = driver.FindElement(By.Id("keyword"));
            keywordField.Click();
            keywordField.SendKeys("home");
            var search = driver.FindElement(By.Id("search"));
            search.Click();
            var resultTitle = driver.FindElement(By.CssSelector("#task2 > tbody > tr.title > td")).Text;

            Assert.That(resultTitle, Is.EqualTo("Home page"));

        }

        [Test]
        public void TestFindTaskByInvalidKeyword()
        {
            var searchButton = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a"));
            searchButton.Click();
            var keywordField = driver.FindElement(By.Id("keyword"));
            keywordField.Click();
            keywordField.SendKeys("invalidTask");
            var search = driver.FindElement(By.Id("search"));
            search.Click();
            var result = driver.FindElement(By.Id("searchResult")).Text;

            Assert.That(result, Is.EqualTo("No tasks found."));

        }

        [Test]
        public void TestCreateTaskHoldingInvalidKeyword()
        {
            var createButton = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
            createButton.Click();
            //var title = driver.FindElement(By.Id("title"));
            //title.Click();
           // title.SendKeys("Add Task");
            var description = driver.FindElement(By.Id("description"));
            description.Click();
            description.SendKeys("API + UI tests");
            var boardName = driver.FindElement(By.Id("boardName"));
            boardName.Click();
            boardName.SendKeys("Open");
            var create = driver.FindElement(By.Id("create"));
            create.Click();

            var result = driver.FindElement(By.CssSelector("body > main > div")).Text;

            Assert.That(result, Is.EqualTo("Error: Title cannot be empty!"));

        }
 [Test]
        public void TestCreateTaskHoldingValidData()
        {
            var createButton = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
            createButton.Click();
            var title = driver.FindElement(By.Id("title"));
            title.Click();
            title.SendKeys("Add Task");
            var description = driver.FindElement(By.Id("description"));
            description.Click();
            description.SendKeys("API + UI tests");
            var boardName = driver.FindElement(By.Id("boardName"));
            boardName.Click();
            boardName.SendKeys("Open");
            var create = driver.FindElement(By.Id("create"));
            create.Click();

            var allTasks = driver.FindElements(By.CssSelector("body > main > div"));
            var lastTask = allTasks.Last();
            var lastTaskTitle = lastTask.FindElement(By.CssSelector("#task21 > tbody > tr.title > td")).Text;
            var lastTaskDescription = lastTask.FindElement(By.CssSelector("#task20 > tbody > tr.description > td > div")).Text;

            Assert.That(lastTaskTitle, Is.EqualTo("Add Task"));
            Assert.That(lastTaskDescription, Is.EqualTo("API + UI tests"));
            
        }
    }
}