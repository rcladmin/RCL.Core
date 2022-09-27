using System.Threading.Tasks;

namespace RCL.Core.Authorization
{
    public interface IAuthTokenService
    {
        Task<AuthToken> GetAuthTokenAsync(string resource);
    } 
}
