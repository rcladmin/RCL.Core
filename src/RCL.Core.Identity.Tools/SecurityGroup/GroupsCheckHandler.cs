#nullable disable

using Microsoft.AspNetCore.Authorization;
using RCL.Core.Identity.Graph;

namespace RCL.Core.Identity.Tools
{
    public class GroupsCheckHandler : AuthorizationHandler<GroupsCheckRequirement>
    {
        private readonly IGraphService _graphClientService;

        public GroupsCheckHandler(IGraphService graphClientService)
        {
            _graphClientService = graphClientService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupsCheckRequirement requirement)
        {
            if (!string.IsNullOrEmpty(context?.User?.Identity?.Name))
            {
                try
                {
                    string objectId = context?.User?.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? String.Empty;

                    bool result = false;

                    if (!string.IsNullOrEmpty(objectId))
                    {
                        List<string> groups = await _graphClientService.GetUserInGroupsByObjectIdAsync(objectId);

                        if (groups?.Count > 0)
                        {
                            foreach (var group in groups)
                            {
                                if (requirement.Groups.Contains(group))
                                {
                                    result = true;
                                }
                            }
                        }
                    }

                    if (result)
                    {
                        context.Succeed(requirement);
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
            }
        }
    }
}
