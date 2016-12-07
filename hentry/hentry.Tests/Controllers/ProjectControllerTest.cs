using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hentry;
using hentry.Controllers;
using Moq;
using System.Diagnostics;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Security.Principal;
using System.Threading;

namespace hentry.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        static IWebDriver driverFF;

        [TestMethod]
        public void Admin_Can_View_Create_Project()
        {
            // Arrange
            var user = new Mock<IPrincipal>();
            user.SetupGet(x => x.Identity.IsAuthenticated).Returns(true);
            user.Setup(x => x.IsInRole("Admin")).Returns(true);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(user.Object);

            var controller = new ProjectController();

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(context.Object);

            controller.ControllerContext = controllerContextMock.Object;

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Verify_That_Project_View_Needs_Authorization()
        {
            // Arrange
            ProjectController controller = new ProjectController();
            Type type = controller.GetType();

            // Act
            var methodInfo = type.GetMethod("Index");
            var attributes = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            Console.WriteLine(attributes.ToString());

            // Assert
            Assert.IsTrue(attributes.ToString().Contains("System.Web.Mvc.AuthorizeAttribute"));
        }

        
        [TestMethod]
        public void Test_Login_With_Firefox()
        {
            // Needs IIS to be started manually first
            driverFF = new FirefoxDriver();
            
            driverFF.Navigate().GoToUrl("http://localhost:3829/Account/Login");
            driverFF.FindElement(By.Id("Email")).SendKeys("test@email.com");
            driverFF.FindElement(By.Id("Password")).SendKeys("Kissa123!");
            driverFF.FindElement(By.Id("Password")).SendKeys(Keys.Enter);

            // Could not find selenium wait
            Thread.Sleep(5000);
            Assert.AreEqual("Home Page - Hentry", driverFF.Title);
            driverFF.Close();
            driverFF.Quit();
        }
        
    }
}
