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

            string dob = User.Claims.FirstOrDefault(c => c.Type == "extension_DateofBirth")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(dob))
            {
                userClaimsData.DateOfBirth = DateTime.ParseExact(dob,"dd/MM/yyyy", null);
            }

            string email = User.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.Email = email;
            }

            string streetAddress = User.Claims.FirstOrDefault(c => c.Type == "streetAddress")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.StreetAddress = streetAddress;
            }

            string city = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.City = city;
            }

            string state = User.Claims.FirstOrDefault(c => c.Type == "state")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.State = state;
            }

            string postalCode = User.Claims.FirstOrDefault(c => c.Type == "postalCode")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                userClaimsData.PostalCode = postalCode;
            }

            string country = User.Claims.FirstOrDefault(c => c.Type == "country")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(country))
            {
                userClaimsData.Country = country;
            }

            string identityApprover = User.Claims.FirstOrDefault(c => c.Type == "extension_IdentityApprover")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(identityApprover))
            {
                userClaimsData.IdentityApprover = identityApprover;
            }

            string objectId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(objectId))
            {
                userClaimsData.ObjectId = objectId;
            }

            string photoURL = User.Claims.FirstOrDefault(c => c.Type == "extension_PhotoUrl")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(photoURL))
            {
                userClaimsData.PhotoUrl = photoURL;
            }

            string dateCreated = User.Claims.FirstOrDefault(c => c.Type == "extension_DateCreated")?.Value ?? string.Empty;
            if (!string.IsNullOrEmpty(dateCreated))
            {
                userClaimsData.DateCreated = DateTime.ParseExact(dateCreated, "dd/MM/yyyy", null);
            }

            return userClaimsData;
        }
    }
}
