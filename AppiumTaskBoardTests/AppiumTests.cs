using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumTaskBoardTests
{
    public class AppiumTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";
        private AppiumOptions options;

        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\TaskBoard.DesktopClient-v1.0\TaskBoard.DesktopClient.exe");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
        }

        [TearDown]
        public void CloseApp()
        {
            this.driver.Quit();
        }

        [Test]
        public void TestSearchTaskByValidTitle()
        {
            var connect = driver.FindElementByAccessibilityId("buttonConnect");
            connect.Click();

            string windowsName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowsName); 

            var textBoxSearch = driver.FindElementByAccessibilityId("textBoxSearchText");
            textBoxSearch.SendKeys("Project skeleton");
            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

            Thread.Sleep(5000); 

            var resultTitle = driver.FindElementByXPath("//ListItem[@Name=\"11\"]").Text;
            Assert.NotNull(resultTitle);
        }

        [Test]
        public void TestAddTaskWithValidTitle()
        {
            var connect = driver.FindElementByAccessibilityId("buttonConnect");
            connect.Click();


            string windowsName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowsName);

            var buttonAdd = driver.FindElementByAccessibilityId("buttonAdd");
            buttonAdd.Click();

            var buttonReload = driver.FindElementByAccessibilityId("buttonReload");

            var textBoxTitle = driver.FindElementByAccessibilityId("textBoxTitle");
            textBoxTitle.SendKeys("Add my unique task");
            var textBoxDescription = driver.FindElementByAccessibilityId("textBoxDescription");
            textBoxDescription.SendKeys("Add new task");
            var createButton = driver.FindElementByAccessibilityId("buttonCreate");
            createButton.Click();
            buttonReload.Click();

            Thread.Sleep(5000);

            var textBoxSearch = driver.FindElementByAccessibilityId("textBoxSearchText");
            textBoxSearch.SendKeys("Add my unique task");
            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

            Thread.Sleep(5000);

            var resultTitle = driver.FindElementByXPath("//ListItem[@Name=\"919\"]").Text;
            var allResult = driver.FindElementsByXPath("/Window/List/Group/ListItem").Count;

            Assert.NotNull(resultTitle);
            Assert.That(allResult, Is.GreaterThan(0));
        }
    }
}