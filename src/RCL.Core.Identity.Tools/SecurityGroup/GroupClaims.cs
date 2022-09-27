using System.Security.Claims;

namespace RCL.Core.Identity.Tools
{
    public static class GroupClaims
    {
        public static List<string> GetGroups(ClaimsPrincipal User)
        {
            List<string> groups = new List<string>();
            try
            {
                string _groups = User.FindFirst(c => c.Type == "aadgroups")?.Value ?? string.Empty;
                if (!string.IsNullOrEmpty(_groups))
                {
                    groups = _groups.Split(',').ToList();
                }
            }
            catch (Exception) { }
            return groups;
        }

        public static bool HasGroup(ClaimsPrincipal User, string ClaimValue)
        {
            bool result = false;

            try
            {
                List<string> groups = GetGroups(User);

                if (groups?.Count > 0)
                {
                    if (groups.Contains(ClaimValue))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }

            return result;
        }
    }
}
