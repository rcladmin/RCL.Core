#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace RCL.Core.Identity.Proofing
{
    public class UserData
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string DisplayName { get; set; }
        public string Country { get; set; }
        public string ApprovalStatus { get; set; }
        public string InputData { get; set; }
        public string Organization { get; set; }
        public string EnrollmentUrl { get; set; }
        public string ApproverEmail { get; set; }
        public string TemporaryPassword { get; set; }
        public string UserPrincipalName { get; set; }
        public string ObjectId { get; set; }

        [NotMapped]
        public List<IdentityData> Identities { get; set; }
    }
}
