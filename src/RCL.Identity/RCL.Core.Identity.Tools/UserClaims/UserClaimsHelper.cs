using System.Security.Claims;

namespace RCL.Core.Identity.Tools
{
    public static class UserClaimsHelper
    {
        public static UserClaimsData GetUserDataFromClaims(ClaimsPrincipal User)
        {
            UserClaimsData userClaimsData = new UserClaimsData();

            string givenName = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value ?? string.Empty;
            if(!string.IsNullOrEmpty(givenName))
            {
                userClaimsData.GivenName = givenName;
            }

            string surName = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(surName))
            {
                userClaimsData.SurName = surName;
            }

            string displayName = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(displayName))
            {
                userClaimsData.DisplayName = displayName;
            }

            string email = User.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.Email = email;
            }

            string streetAddress = User.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.StreetAddress = streetAddress;
            }

            string city = User.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.City = city;
            }

            string state = User.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.State = state;
            }

            string postalCode = User.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.PostalCode = postalCode;
            }

            string country = User.Claims.FirstOrDefault(c => c.Type == "country")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(country))
            {
                userClaimsData.Country = country;
            }

            string objectId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(objectId))
            {
                userClaimsData.ObjectId = objectId;
            }

            return userClaimsData;
        }
    }
}
