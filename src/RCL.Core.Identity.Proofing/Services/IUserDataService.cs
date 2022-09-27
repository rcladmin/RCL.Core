namespace RCL.Core.Identity.Proofing
{
    public interface IUserDataService
    {
        Task<UserData> GetUserDataByIdAsync(string subscrid, int id);
        Task<List<UserData>> GetUserDataBySubscriptionAsync(string subscrid);
        Task ApproveUserDataAsync(string subscrid, UserData userData);
        Task DeleteUserDataAsync(string subscrid, int id);
    }
}
