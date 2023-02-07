#nullable disable

using Microsoft.Graph;

namespace RCL.Core.Identity.Graph
{
    public interface IGraphService
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserByPrincipalNameAsync(string principalName);
        Task<User> GetUserByObjectIdAsync(string objectId);
        Task<List<string>> GetUserInGroupsByObjectIdAsync(string objectId);
        Task<List<User>> GetUsersByNameAsync(string givenName, string surname);
        Dictionary<string, object> GetUserCustomAttributes(User user);
        Task DeleteUserByObjectIdAsync(string objectId);
    }
}
