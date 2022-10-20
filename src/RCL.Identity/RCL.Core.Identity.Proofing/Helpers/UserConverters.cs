#nullable disable

using Microsoft.Graph;
using System.Text.Json;

namespace RCL.Core.Identity.Proofing
{
    public static class UserConverter
    {
        public static User ConvertToGraphUser(UserData userData)
        {
            RegisterForApprovalInputModel inputClaims =
                JsonSerializer.Deserialize<RegisterForApprovalInputModel>(userData.InputData);

            User user = new User
            {
                GivenName = userData.GivenName,
                Surname = userData.SurName,
                DisplayName = userData.DisplayName,
                StreetAddress = userData.StreetAddress,
                City = userData.City,
                State = userData.StateProvince,
                PostalCode = userData.PostalCode,
                Country = userData.Country,
                Identities = new List<ObjectIdentity>()
                {
                    new ObjectIdentity
                    {
                        SignInType = inputClaims.identities[0].signInType,
                        Issuer = inputClaims.identities[0].issuer,
                        IssuerAssignedId = inputClaims.identities[0].issuerAssignedId
                    }
                },
                PasswordProfile = new PasswordProfile()
                {
                    Password = PasswordHelper.GenerateNewPassword(4, 8, 4),
                    ForceChangePasswordNextSignIn = true
                },
                PasswordPolicies = "DisablePasswordExpiration",
            };
            user.AdditionalData = new Dictionary<string, object>();
            user.AdditionalData.Add($"extension_{userData.B2CExtensionAppId}_DateofBirth", userData.DateOfBirth.ToString("dd/MM/yyyy"));
            user.AdditionalData.Add($"extension_{userData.B2CExtensionAppId}_IdentityApprover", userData.IdentityApprover);

            return user;
        }
    }
}
