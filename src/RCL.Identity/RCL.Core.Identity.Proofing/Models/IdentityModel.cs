#nullable disable

namespace RCL.Core.Identity.Proofing
{
    internal class IdentityModel
    {
        public string signInType { get; set; }

        public string issuer { get; set; }

        public string issuerAssignedId { get; set; }
    }
}
