namespace RCL.Core.Identity.Proofing
{
    public interface IUserDataService
    {
        Task<UserData> GetUserDataByIdAsync(string subscrid, int id);
        Task<List<UserData>> GetUserDataByIdentityApproverAsync(string subscrid, string identityApproverIdentifier);
        Task ApproveUserDataAsync(string subscrid, UserData userData);
        Task<UserData> UpdateUserDataAsync(string subscrid, int id, UserData userData);
        Task DeleteUserDataAsync(string subscrid, int id);
    }
}
