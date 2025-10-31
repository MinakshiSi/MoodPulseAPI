using Microsoft.AspNetCore.Mvc;
using MoodService.Models;
using MoodService.Services;
using Microsoft.ApplicationInsights;

namespace MoodService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodController : ControllerBase
    {
        private readonly IMoodService _moodService;
        private readonly TelemetryClient _telemetry;

        public MoodController(IMoodService moodService, TelemetryClient telemetry)
        {
            _moodService = moodService;
            _telemetry = telemetry;
            _telemetry.InstrumentationKey = "29108a9c-0b57-4b5b-a568-3f1b8b3d8d95";
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var moods = await _moodService.GetAllAsync();
            return Ok(moods);
        }

        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] MoodEntry entry)
        //{
        //    await _moodService.AddEntryAsync(entry);
        //    return CreatedAtAction(nameof(GetAll), new { id = entry.Id }, entry);
        //}
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MoodEntry entry)
        {
            await _moodService.AddEntryAsync(entry);
            var syntheticUserId = Guid.NewGuid().ToString();

            // 🔹 Track custom event
            _telemetry.TrackEvent("MoodSubmitted", new Dictionary<string, string>
        {
            { "Mood", entry.Emotion },
             { "SyntheticUserId", syntheticUserId },
            { "Timestamp", DateTime.UtcNow.ToString("o") }
        });

            return CreatedAtAction(nameof(GetAll), new { id = entry.Id }, entry);
        }
    }
}
