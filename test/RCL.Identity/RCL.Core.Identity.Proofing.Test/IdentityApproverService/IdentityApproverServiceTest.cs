namespace RCL.Core.Identity.Proofing.Test.OrganizationDataService
{
    [TestClass]
    public class IdentityApproverServiceTest
    {
        private readonly IIdentityApproverService _identityApproverService;

        public IdentityApproverServiceTest()
        {
            _identityApproverService = (IIdentityApproverService)DependencyResolver.ServiceProvider()
                .GetService(typeof(IIdentityApproverService));
        }

        [TestMethod]
        public async Task GetIdentityApproverByIdentifierTest()
        {
            try
            {
                IdentityApprover identityApprover = await _identityApproverService.GetIdentityApproverByIdentifierAsync("I-TKGDBEFH2AZM", "Contoso");
                Assert.AreNotEqual(String.Empty, identityApprover?.Name ?? String.Empty);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
