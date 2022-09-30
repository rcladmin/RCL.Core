namespace RCL.Core.Identity.Proofing
{
    public interface IOrganizationDataService
    {
        Task<OrganizationData> CreateOrganizationAsync(string subscrid, OrganizationData organization);
        Task<OrganizationData> UpdateOrganizationDataAsync(string subscrid, OrganizationData organization);
        Task<OrganizationData> GetOrganizationDataByIdAsync(string subscrid, int id);
        Task<OrganizationData> GetOrganizationDataBySubscriptionAsync(string subscrid, int subscribid);
        Task DeleteOrganizationDataAsync(string subscrid, int id);
    }
}
