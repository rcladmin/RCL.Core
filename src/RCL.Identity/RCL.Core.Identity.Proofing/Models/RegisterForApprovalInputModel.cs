#nullable disable

namespace RCL.Core.Identity.Proofing
{
    internal class RegisterForApprovalInputModel
    {
        public string email { get; set; }

        public List<IdentityModel> identities { get; set; }

        public string displayName { get; set; }

        public string givenName { get; set; }

        public string surName { get; set; }

        public string country { get; set; }

        public string governmentId { get; set; }

        public string inputData { get; set; }

        public UserData CreateUserData()
        {
            var data = new UserData();
            data.Email = email;
            data.DisplayName = displayName;
            data.GivenName = givenName;
            data.SurName = surName;
            data.Country = country;
            data.Identities = identities?.Select(x => new IdentityData() { Issuer = x.issuer, IssuerAssignedId = x.issuerAssignedId, SignInType = x.signInType }).ToList();
            data.InputData = inputData;
            data.ApprovalStatus = Constants.UserApprovalStatus.Pending;

            return data;
        }
    }
}