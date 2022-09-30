using Microsoft.Graph;

namespace RCL.Core.Identity.Graph.Test
{
    [TestClass]
    public class GraphServiceTest
    {
        private readonly IGraphService _graphService;

        public GraphServiceTest()
        {
            _graphService = (IGraphService)DependencyResolver.ServiceProvider()
                .GetService(typeof(IGraphService));
        }

        [TestMethod]
        public async Task CreateUserTest()
        {
            try
            {
                User user = new User
                {
                    AccountEnabled = true,
                    DisplayName = "John Doe",
                    MailNickname = "JohnD",
                    UserPrincipalName = "johndoe@rcldemob2c.onmicrosoft.com",
                    PasswordProfile = new PasswordProfile { Password = "xWwvJ]6NMw+bWH-d", ForceChangePasswordNextSignIn = true }
                };

                User newUser = await _graphService.CreateUser(user);

                Assert.AreNotEqual(string.Empty, newUser?.Id ?? String.Empty);
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetUserByPrincipalNameTest()
        {
            try
            {
                Microsoft.Graph.User user = await _graphService.GetUserByPrincipalNameAsync("5d1a093f-bd94-41dc-ae6d-862d73b90b88@rcldemob2c.onmicrosoft.com");

                Assert.AreNotEqual(string.Empty, user?.Id ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetUserByObjectIdTest()
        {
            try
            {
                Microsoft.Graph.User user = await _graphService.GetUserByObjectIdAsync("9a95407c-2ffc-417c-b8ff-3b0a01aa71c3");

                Assert.AreNotEqual(string.Empty, user?.Id ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetUserInGroupsByObjectIdTest()
        {
            try
            {
                List<string> groups = await _graphService.GetUserInGroupsByObjectIdAsync("f8dcffd3-aa33-4895-8c1a-8f3c3dfd38cd");

                Assert.AreNotEqual(0, groups.Count);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
