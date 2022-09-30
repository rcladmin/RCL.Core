using Microsoft.AspNetCore.Authorization;

namespace RCL.Core.Identity.Tools
{
    public class GroupsCheckRequirement :  IAuthorizationRequirement
    {
        public string[] Groups { get; set; }

        public GroupsCheckRequirement(string[] groups)
        {
            Groups = groups;
        }
    }
}
