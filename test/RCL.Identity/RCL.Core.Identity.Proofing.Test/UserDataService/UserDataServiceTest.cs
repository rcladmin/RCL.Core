namespace RCL.Core.Identity.Proofing.Test.UserDataService
{
    [TestClass]
    public class UserDataServiceTest
    {
        private readonly IUserDataService _userDataService;
        private const string _subscrid = "I-TKGDBEFH2AZM";

        public UserDataServiceTest()
        {
            _userDataService = (IUserDataService)DependencyResolver.ServiceProvider()
                .GetService(typeof(IUserDataService));
        }

        [TestMethod]
        public async Task GetUserDataByIdTest()
        {
            try
            {
                UserData userData = await _userDataService.GetUserDataByIdAsync(_subscrid, 1);
                Assert.AreNotEqual(String.Empty, userData?.DisplayName ?? String.Empty);
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetUserDataBySubsciptionTest()
        {
            try
            {
                List<UserData> userData = await _userDataService.GetUserDataByIdentityApproverAsync(_subscrid,"Contoso");
                Assert.AreNotEqual(0, userData?.Count);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task DeleteUserDataTest()
        {
            try
            {
                await _userDataService.DeleteUserDataAsync(_subscrid,1);
                Assert.AreEqual(1, 1);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
