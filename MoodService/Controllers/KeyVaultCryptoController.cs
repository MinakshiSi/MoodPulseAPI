
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace MoodService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyVaultCryptoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Uri _vaultUri;
        private readonly string _keyName = "MoodPulseKey";

        public KeyVaultCryptoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _vaultUri = new Uri(_configuration["KeyVault:VaultUri"]);
        }

        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt([FromBody] string plaintext)
        {
            var keyClient = new KeyClient(_vaultUri, new DefaultAzureCredential());
            KeyVaultKey key = await keyClient.GetKeyAsync(_keyName);

            var cryptoClient = new CryptographyClient(key.Id, new DefaultAzureCredential());
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            EncryptResult result = await cryptoClient.EncryptAsync(EncryptionAlgorithm.RsaOaep, plaintextBytes);
            string encryptedBase64 = Convert.ToBase64String(result.Ciphertext);

            return Ok(new { Encrypted = encryptedBase64 });
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt([FromBody] string encryptedBase64)
        {
            var keyClient = new KeyClient(_vaultUri, new DefaultAzureCredential());
            KeyVaultKey key = await keyClient.GetKeyAsync(_keyName);

            var cryptoClient = new CryptographyClient(key.Id, new DefaultAzureCredential());
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);

            DecryptResult result = await cryptoClient.DecryptAsync(EncryptionAlgorithm.RsaOaep, encryptedBytes);
            string decryptedText = Encoding.UTF8.GetString(result.Plaintext);

            return Ok(new { Decrypted = decryptedText });
        }
    }

}
