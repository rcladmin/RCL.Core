namespace RCL.Core.Api.Authorization
{
    public interface IAuthorizationFactory
    {
        IApiAuthorization Create(AuthType authType);
    }
}
