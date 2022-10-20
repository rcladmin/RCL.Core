#nullable disable

namespace RCL.Core.Identity.Proofing
{
    public class IdentityApprover
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Email { get; set; }
        public string EnrollmentInstructionsUrl { get; set; }
        public string EnrollmentUrl { get; set; }
    }
}
