
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
namespace MoodService.Services
{
    public class KeyVaultService
    {
        private readonly IConfiguration _configuration;

        public KeyVaultService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetKeyInfoAsync()
        {
            var vaultUri = new Uri(_configuration["KeyVault:VaultUri"]);
            var keyClient = new KeyClient(vaultUri, new DefaultAzureCredential());

            // Get the key
            KeyVaultKey key = await keyClient.GetKeyAsync("MoodPulseKey");

            // Display key info
            return $"Key Name: {key.Name}, Type: {key.KeyType}, Created On: {key.Properties.CreatedOn}";
        }
    }

}
