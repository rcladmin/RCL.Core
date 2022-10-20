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
                    MailNickname = "JohnD",
                    UserPrincipalName = "johndoe@contoseco.onmicrosoft.com",
                    PasswordProfile = new PasswordProfile { Password = "xWwvJ]6NMw+bWH-d", ForceChangePasswordNextSignIn = true },
                    GivenName = "John",
                    Surname = "Doe",
                    DisplayName = "John Doe",
                    StreetAddress = "Any sreet",
                    City = "Any city",
                    State = "Any state",
                    Country = "Trinidad and Tobago",
                    PostalCode = "500000"
                };

                user.AdditionalData = new Dictionary<string, object>();

                user.AdditionalData.Add($"extension_9e8a867f94e04a01bbaba86fbdec55cb_DateofBirth", "14/02/1990");
                user.AdditionalData.Add($"extension_9e8a867f94e04a01bbaba86fbdec55cb_IdentityApprover", "Contoso");

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
                Microsoft.Graph.User user = await _graphService.GetUserByObjectIdAsync("b5edeb18-83ec-4849-bef6-e4b057944c6e");

                Assert.AreNotEqual(string.Empty, user?.Id ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetUserCustomAttributesTest()
        {
            try
            {
                Microsoft.Graph.User user = await _graphService.GetUserByObjectIdAsync("b5edeb18-83ec-4849-bef6-e4b057944c6e");
                Dictionary<string, object> customAttributes = _graphService.GetUserCustomAttributes(user);

                Assert.AreNotEqual(0,customAttributes?.Count);
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

        [TestMethod]
        public async Task GetUsersByNameTest()
        {
            try
            {
                List<User> users = await _graphService.GetUsersByNameAsync("John", "Doe");

                Assert.AreNotEqual(0, users.Count);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
