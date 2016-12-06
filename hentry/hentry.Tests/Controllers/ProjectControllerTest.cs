using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hentry;
using hentry.Controllers;
using Moq;
using System.Diagnostics;
using System.Web;
using System.Security.Principal;

namespace hentry.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
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

    }
}
