namespace RCL.Core.Identity.Proofing
{
    public interface IIdentityApproverService
    {
        Task<IdentityApprover> GetIdentityApproverByIdentifierAsync(string subscrid, string identifier);
    }
}
