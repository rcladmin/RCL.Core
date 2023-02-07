#nullable disable

using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace RCL.Core.Identity.Tools
{
    public class GroupClaimsTransformation : IClaimsTransformation
    {
        private readonly IGraphService _graphClientService;

        public GroupClaimsTransformation(IGraphService graphClientService)
        {
            _graphClientService = graphClientService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            try
            {
                var claimType = "aadgroups";
                if (!principal.HasClaim(claim => claim.Type == claimType))
                {
                    string objectId = principal?.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;
                    if (!string.IsNullOrEmpty(objectId))
                    {
                        List<string> groups = await _graphClientService.GetUserInGroupsByObjectIdAsync(objectId);

                        if (groups?.Count > 0)
                        {
                            claimsIdentity.AddClaim(new Claim(claimType, string.Join(",", groups)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            principal.AddIdentity(claimsIdentity);
            return principal;
        }
    }
}
