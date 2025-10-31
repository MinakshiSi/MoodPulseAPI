using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodService.Models;

namespace MoodService.Controllers
{


    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {

        private readonly TelemetryClient _telemetry;
        public ImageController(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
            _telemetry.InstrumentationKey = "29108a9c-0b57-4b5b-a568-3f1b8b3d8d95";
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListImages()
        {
            var blobUri = new Uri("https://yourstorageaccount.blob.core.windows.net/images");
            var credential = new DefaultAzureCredential(); // Uses signed-in user's identity
            var containerClient = new BlobContainerClient(blobUri, credential);

            var blobs = new List<string>();
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem.Name);
            }
            _telemetry.TrackEvent("BlobListed", new Dictionary<string, string> {
                { "User", User.Identity.Name },
                { "Container", "images" }
            });


            return Ok(blobs);
        }
    }

}
