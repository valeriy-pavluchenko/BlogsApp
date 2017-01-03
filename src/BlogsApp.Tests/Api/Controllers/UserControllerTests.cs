using BlogsApp.Api.Controllers;
using BlogsApp.Api.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogsApp.Tests.Api.Controllers
{
    [TestClass]
    public class UserControllerTests : UnitTestsSetup
    {
        [TestMethod]
        public void GetUsersList_NormalWorkflow_UsersListIsReturned()
        {
            var controller = new UserController(Repositories);
            var parameters = new ListParameters
            {
                Limit = int.MaxValue,
                Offset = 0
            };

            var usersResponse = controller.GetUsersList(parameters);
            Assert.IsNotNull(usersResponse);
        }
    }
}
