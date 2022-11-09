namespace RCL.Core.Azure.Storage
{
    public class AzureBlobStorageOptions
    {
        public AzureBlobStorageOptions()
        {
        }

        public AzureBlobStorageOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
