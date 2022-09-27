namespace RCL.Core.Authorization
{
    internal class AuthServerOptions
    {
        public string grant_type { get; set; }
        public string endpoint { get; set; }
        public string resource { get; set; }
    }
}
