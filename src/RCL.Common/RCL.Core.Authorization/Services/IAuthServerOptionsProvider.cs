using System.Threading.Tasks;

namespace RCL.Core.Authorization
{
    internal interface IAuthServerOptionsProvider
    {
        Task<AuthServerOptions> GetServiceOptionsAsync(string resource);
    }
}
