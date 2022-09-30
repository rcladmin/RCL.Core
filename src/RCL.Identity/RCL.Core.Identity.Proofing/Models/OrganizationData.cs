#nullable disable

namespace RCL.Core.Identity.Proofing
{
    public class OrganizationData
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public string OrganizationName { get; set; }
        public string ApproverEmail { get; set; }
        public string EnrollmentInstructionsUrl { get; set; }
        public string EnrollmentUrl { get; set; }
    }
}
