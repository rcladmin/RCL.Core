using System.Threading.Tasks;

namespace RCL.Core.Authorization
{
    internal interface IAuthClientOptionsProvider
    {
        Task<AuthClientOptions> GetClientOptionsAsync();
    }
}
