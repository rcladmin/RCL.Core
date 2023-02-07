#nullable disable

namespace RCL.Core.Identity.Tools
{
    public class UserClaimsData
    {
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string IdentityApprover { get; set; }
        public string ObjectId { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
