namespace RCL.Core.Identity.Proofing.Test.OrganizationDataService
{
    [TestClass]
    public class OrganizationDataServiceTest
    {
        private readonly IOrganizationDataService _organizationDataService;
        private const string _subscrid = "xxx";
        private OrganizationData _organizationData = new OrganizationData
        {
            OrganizationName = "Contoso Eco Resort",
            ApproverEmail = "support@xxx.com",
            EnrollmentUrl = "https://rcl-demo-identity-enrollment.azurewebsites.net/",
            EnrollmentInstructionsUrl = "https://rcl-demo-identity-enrollment.azurewebsites.net/Instructions",
            SubscriptionId = 00
        };

        public OrganizationDataServiceTest()
        {
            _organizationDataService = (IOrganizationDataService)DependencyResolver.ServiceProvider()
                .GetService(typeof(IOrganizationDataService));
        }

        [TestMethod]
        public async Task CreateOrganizationDataTest()
        {
            try
            {
                OrganizationData organizationData = await _organizationDataService.CreateOrganizationAsync(_subscrid, _organizationData);
                Assert.AreEqual(_organizationData.OrganizationName, organizationData?.OrganizationName ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task UpdateOrganizationDataTest()
        {
            try
            {
                _organizationData.Id = 3;
                _organizationData.ApproverEmail = "rcl@xxx.com";
                OrganizationData organizationData = await _organizationDataService.UpdateOrganizationDataAsync(_subscrid, _organizationData);
                Assert.AreEqual(_organizationData.OrganizationName, organizationData?.OrganizationName ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetOrganizationDataByIdTest()
        {
            try
            {
                OrganizationData organizationData = await _organizationDataService.GetOrganizationDataByIdAsync(_subscrid, 2);
                Assert.AreNotEqual(String.Empty, organizationData?.OrganizationName ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetOrganizationDataBySubscriptionTest()
        {
            try
            {
                OrganizationData organizationData = await _organizationDataService.GetOrganizationDataBySubscriptionAsync(_subscrid, 25);
                Assert.AreNotEqual(String.Empty, organizationData?.OrganizationName ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task DeleteOrganizationDataTest()
        {
            try
            {
                await _organizationDataService.DeleteOrganizationDataAsync(_subscrid, 2);
                Assert.AreEqual(1,1);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
