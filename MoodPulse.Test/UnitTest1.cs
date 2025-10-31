using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using System.Security.Cryptography.X509Certificates;
using Xunit;
using Xunit.Abstractions;

namespace MoodPulse.Test
{
    public class KeyVaultCertificateTests
    {
        private readonly Uri _vaultUri = new Uri("https://MoodPulseVault13909.vault.azure.net/");
        private readonly string _certName = "MoodPulseCert";
        private readonly ITestOutputHelper _output;

        public KeyVaultCertificateTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task LoadCertificate_ShouldReturnValidX509()
        {
            // Arrange
            var certClient = new CertificateClient(_vaultUri, new DefaultAzureCredential());

            // Act
            var certBundle = await certClient.GetCertificateAsync(_certName);
            var x509 = new X509Certificate2(certBundle.Value.Cer);

            _output.WriteLine("Certificate Bundle: "+ certBundle.ToString() );

            // Assert
            Assert.NotNull(x509);
            Assert.Contains("CN=", x509.Subject); // Basic check for subject
            Assert.True(x509.NotAfter > DateTime.UtcNow); // Check expiry
        }
    }

}